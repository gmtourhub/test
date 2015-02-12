var defaultMenu = 'posts-all';
var postChange = false;
var postSettingHeaderHeight = 40;

$.fn.isScrollable = function () {
    var elem = $(this);
    return (
    elem.css('overflow') == 'scroll'
        || elem.css('overflow') == 'auto'
        || elem.css('overflow-x') == 'scroll'
        || elem.css('overflow-x') == 'auto'
        || elem.css('overflow-y') == 'scroll'
        || elem.css('overflow-y') == 'auto'
    );
};

$(document).ready(function () { blog.ready(); });

var blog = {
    ready: function () {        
        blog.htmlEditor();
        blog.bindElement();
        blog.initializeElement();
        blog.anchorOnLoad();        
        blog.displayBlogList(blog.getFullHash(), blog.getHash(), '');
        blog.categoryFocus();
        blog.blockManagementSubmit();
        blog.cloneBlogHeader();        
    },
    cloneBlogHeader: function () {
        var headerName = $('.remove-link').text();
        if (headerName != '') {
            $('.blog-header-freeze').remove();
            $('.master-body-wrapper').append('<div class="blog-header-freeze"><span class="two-box">2Box</span>' + headerName + '</div>');
            $('.blog-header-freeze').hide();
        }
    },
    categoryFocus: function () {
        var isCategory = window.location.toString().toLowerCase().indexOf('/category/') > -1;
        if (isCategory) {
            var urlSplit = window.location.toString().split('/');
            var categoryName = urlSplit[urlSplit.length - 1].replace(/-/g,' ');
            $('.display-categories li a').filter(function (idx) {
                return $(this).text().replace(/›/g,'') == categoryName;
            }).addClass('link-focus');
        }
    },
    blockManagementSubmit:function(){
        var isManagement = window.location.toString().toLowerCase().indexOf('blogger.aspx') > -1;
        if (isManagement) $('form').bind('submit', function () { return false; });
    },
    getHash: function () {
        var hash = window.location.hash;
        var hash = hash == '' ? 'ALL' : (hash.split('-').length == 1 ? hash.split('-')[0] : hash.split('-')[1]);
        return hash;
    },
    getFullHash: function () {
        return window.location.hash;      
    },
    bindAnchor:function(obj){
        blog.refreshIcon();
        var anchor = $(obj).attr('href').replace('#', '');        
        blog.anchorDisplay(anchor);        
        blog.displayBlogList(anchor,anchor.split('-')[1],'');
    },
    bindElement: function () {
        $(window).scroll(function () {
            if ($(window).scrollTop() >= 150)
                $('.blog-header-freeze').show();
            else
                $('.blog-header-freeze').hide();
        });
        if ($('input[id$="hdfFullContent"]').length > 0 && $('input[id$="hdfFullContent"]').val() == 'N') {
            $('.post-header a').unbind('click').bind('click', function () {
                var key = $(this).attr('key');
                utilities.ajaxPost('Login.aspx/keepViewLog', JSON.stringify({ BlogKey: key }), null, function () {
                }, null);
                if (!(window.location.pathname.toLowerCase() == '/2box/')) {
                    utilities.ajaxPost('../Login.aspx/keepViewLog', JSON.stringify({ BlogKey: key }), null, function () {
                    }, null);                
                }
            });
        }
        // windows resize event for set panel
        $(window).on('resize', blog.initializeElement);
        // set input type text autocomplete off
        $('input').attr('autocomplete', 'off');
        // display anchor menu on load
        blog.anchorDisplay(defaultMenu);
        // bind menu click
        $('a[href^="#"]').on('click', function () {
            blog.checkManagementSessionExpired();
            $('.search-post').val('');
            blog.bindAnchor(this);
        });

        //$('form').submit(function () { return false; });

        $('.save-blog,.publish-blog').on('click', function () {
            if (blog.checkPostBlog() == false) return false;
            blog.checkManagementSessionExpired();
            var curButton = $(this);
            utilities.dialogConfirm('Confirm to save blog', 'Do you want to save this blog?', 'Save', function () {
                $('.save-blog').text('Saving...');
                var typeOfButton = curButton.val();
                blog.postBlog(typeOfButton,false);
            });            
        });

        $('.preview-blog').on('click', function () {
            if (blog.checkPostBlog() == false) return false;
            blog.checkManagementSessionExpired();
            $('.save-blog').text('Saving...');
            blog.postBlog('Save',true);
        });

        $('.close-blog').on('click', function () {
            if (postChange) {                
                utilities.dialogConfirm('Confirm to close', 'Are you sure to close without save?', 'Close', function () {
                    window.location = 'Blogger.aspx';
                })
            } else {
                window.location = 'Blogger.aspx';
            }
        });

        if (typeof (CKEDITOR) !== "undefined") {
            if($('#txtContent').length > 0)
                CKEDITOR.instances.txtContent.on('key', function (event) { postChange = true; });
        }

        $('.post-panel-right .header').bind('click', function () {
            $('.post-panel-right .header').css('border-left', 'solid 3px #fff');
            $(this).css('border-left', 'solid 3px #ff9d25');            
            $(this).parent().parent().find('.setting-box').css('background-color', '#ffffff').height(postSettingHeaderHeight);            
            if ($(this).parent().height() <= postSettingHeaderHeight + 2) {
                var hasRadio = $(this).parent().find('input[type="radio"]').length > 0;
                var automaticCheck = false;
                if (hasRadio) {                    
                    automaticCheck = $(this).parent().find('input[id$="rdbAutomatic"]').is(':checked');
                }
                var cloneDiv = $(this).parent().clone();                
                cloneDiv.css('height', 'auto').insertAfter($(this).parent());
                var autoHeight = cloneDiv.height();
                cloneDiv.remove();
                if (hasRadio) {
                    var radioID = automaticCheck ? 'rdbAutomatic' : 'rdbSetDateAndTime';
                    $(this).parent().find('input[id$="' + radioID + '"]').prop('checked', true);
                }                
                $(this).parent().animate({ height: autoHeight }, 'medium', function () { $(this).css('background-color', '#fff8ed'); if (hasRadio) $('.schedule input[type="radio"]:checked').click(); });
            } else $(this).parent().animate({ height: postSettingHeaderHeight }, 'medium', function () { $(this).css('background-color', '#ffffff'); });
        });

        if ($('.calendar').length > 0) { $(".calendar").datetimepicker({ format: 'd/m/Y H:i' }); $(".calendar").on('keydown', function () { return false; }); }       

        $('.schedule input[type="radio"]').on('click', function () {
            var automaticCheck = false;
            var id = $(this).attr('id');
            if (id.indexOf('rdbSetDateAndTime') > -1) {
                $('.schedule .calendar').show();
                $(this).prop('checked', true);
            }
            else {
                automaticCheck = true;
                $('.schedule .calendar').hide();
            }
            var cloneDiv = $(this).parent().clone();
            var temp = cloneDiv.css('height', 'auto').appendTo($(this).parent().parent());
            var autoHeight = temp.height();
            cloneDiv.remove();
            var radioID = automaticCheck ? 'rdbAutomatic' : 'rdbSetDateAndTime';
            $(this).prop('checked', true);
            $(this).parent().animate({ height: autoHeight }, 'medium');
        });

        $('input[id$="chkSelectAll"]').on('click', function () {
            $('.grid-display-blog input[type="checkbox"],.grid-display-comment input[type="checkbox"]').prop('checked', $(this).is(':checked'));
            var className = 'row-active';
            if ($(this).is(':checked')) $('.grid-display-blog tr,.grid-display-comment tr').addClass(className);
            else $('.grid-display-blog tr,.grid-display-comment tr').removeClass(className);
        });

        $('.button-delete').on('click', blog.deleteAllCheck);

        $('.update-status').on('click', function () {
            blog.checkManagementSessionExpired();
            blog.updateCheckStatus($(this).attr('status'));
        });

        $('.update-private').on('click', function () {
            blog.checkManagementSessionExpired();
            blog.updateCheckPrivate($(this).attr('status'));
        });

        $('.comment-delete-all').on('click', blog.deleteComment);

        $('.comment-publish-all,.comment-spam-all').on('click', function () {
            blog.setCommentType($(this).attr('status'));
        });

        $('.search-post').on('keydown', function (e) {
            if (e.keyCode == 13) {
                blog.displayBlogList(blog.getFullHash(),'',$(this).val());
            }
        });

        $('.content .wrapper').each(function () {
            if ($('input[id$="hdfFullContent"]').val() == 'N') {
                var height = $(this).parent().height();
                $(this).height(height - 25);
                var postComment = $('.post-comment');
                postComment.prev().remove();
                postComment.remove();
            } else {
                $(this).parent().css('height', 'auto').css('max-height','none');
                $(this).css('height', 'auto');
                $('.post-header a').removeAttr('href').addClass('remove-link');
                blog.displayComment(function () {
                    $('.comment-submit-box').show();
                    utilities.checkScrollbar();
                });
            }
        });
      
        $('textarea[id*="txtComment"]').keydown(function (e) {
            var rowCount = utilities.countTextAreaLine(this);            
            if (e.keyCode == 13 || e.keyCode == 8 || e.keyCode == 46 || rowCount <= 0) if (rowCount > 2) $(this).attr('rows', rowCount + 1);
        });

        $('.submit-comment').on('click', function () {
            blog.postComment(this);
        });

        $('.image-box .delete').on('click', function () {
            var parent = $(this).parent();
            var fileName = parent.find('.file-name').text();
            utilities.dialogConfirm('Confirm to delete', 'Are you sure you want to delete this file?', 'Delete', function () {
                utilities.ajaxPost('../Management/FileManager.aspx/DeleteFile', JSON.stringify({ FileName: fileName }), function () {
                    $('.delete').fadeOut('medium');
                }, function (response) {
                    $('.delete').fadeIn('fast');
                    parent.remove();
                }, null);
            });
        });
    },
    initializeElement: function () {        
        var windowHeight = $(window).height();
        var windowWidth = $(window).width();
        
        var headerHeight = $('.management-header').length > 0 ? $('.management-header').innerHeight() : 0;
        var bloggerHeaderHeight = $('.blogger-header').length > 0 ? $('.blogger-header').innerHeight() : 0;
        var myBlogHeight = $('.myblog-bar').length > 0 ? $('.myblog-bar').innerHeight() : 0;
        var height = windowHeight - headerHeight - bloggerHeaderHeight - myBlogHeight -10;

        if ($('.management-panel-left').length > 0) $('.management-panel-left').height(height);

        var panelLeftWidth = $('.management-panel-left').length > 0 ? $('.management-panel-left').innerWidth() : 0;
        var width = windowWidth - panelLeftWidth -10;

        if ($('.management-panel-right').length > 0) $('.management-panel-right').height(height).width(width);
        
        var blogMenuWidth = $('.blog-menus').length > 0 ? $('.blog-menus').innerWidth() : 0;            
        width = windowWidth - blogMenuWidth - 130;
        if ($('.post-name').length > 0) $('.post-name').width(width);

        height = windowHeight - bloggerHeaderHeight -85;
        if ($('.post-panel-left').length > 0) $('.post-panel-left').height(height);
        if ($('.post-panel-right').length > 0) $('.post-panel-right').height(height);

        var postPanelRightWidth = $('.post-panel-right').length > 0 ? $('.post-panel-right').innerWidth() : 0;
        width = windowWidth - postPanelRightWidth;
        var cke_top_height = $('.cke_top').length > 0 ? $('.cke_top').innerHeight() : 0;        

        if ($('.post-panel-left').length > 0) {
            //CKEDITOR.config.height = height - 105;
            $('.post-panel-left').width(width);
        }        

    },
    refreshIcon: function () {
        var link = document.createElement('link');
        link.type = 'image/x-icon';
        link.rel = 'shortcut icon';
        link.href = 'http://www.gmtour.com/favicon.ico';
        document.getElementsByTagName('head')[0].appendChild(link);
    },
    anchorDisplay: function (anchor) {
        if (anchor == '') anchor = defaultMenu;
        if ($('.myblog-bar').length > 0) {
            $('a[href^="#"]').removeClass('active');
            var currentElem = $('a[href^="#' + anchor + '"]');
            currentElem.addClass('active');
            var group = anchor.split('-')[0];
            $('.' + group).addClass('active');
            $(currentElem).parent().find('[class*="-header"]').each(function () {
                var overrideImage = $(this).css('background-image').replace(/-active/g,'');
                $(this).css('background-image',overrideImage);
            });
            var header = $(currentElem).parent().find('.'+group+'-header');
            var image = header.css('background-image').replace(group + '.png', group + '-active.png');
            header.css('background-image', image);

            $('.group-name').text(header.text());
            $('.menu-name').text(currentElem.last().text());

            if (anchor != 'posts-all') $('.search-post').stop(true, true).fadeOut('fast');
            else $('.search-post').stop(true, true).fadeIn('fast');
        }
    },
    anchorOnLoad: function () {
        blog.anchorDisplay(window.location.hash.replace('#',''));
    },
    htmlEditor: function () {
        var windowHeight = $(window).height();
        var bloggerHeaderHeight = $('.blogger-header').length > 0 ? $('.blogger-header').innerHeight() : 0;
        var height = windowHeight - bloggerHeaderHeight - 87;
        $('.ckeditor').each(function () {
            CKEDITOR.replace($(this).attr('id'), {
                toolbarGroups: [
                            { name: 'document', groups: ['mode', 'document', 'doctools'] },
                            { name: 'clipboard', groups: ['clipboard', 'undo'] },
                            { name: 'editing', groups: ['find', 'selection', 'spellchecker'] },
                            { name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
                            { name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi'] },
                            { name: 'links' },
                            { name: 'insert' },
                            { name: 'styles' },
                            { name: 'colors' },
                            { name: 'tools' },
                            { name: 'others' }
                ],
                uiColor: '#F7F7F7',
                enterMode: CKEDITOR.ENTER_BR,
                height: (height - 105) + 'px'
            });
        });        
    },
    postBlog: function (type,isPreview) {        
        var postTitle = $('input[id$="txtPostTitle"]').val();
        var postContent = CKEDITOR.instances.txtContent.getData();
        var schedule = $('input[id$="rdbAutomatic"]').is(':checked') ? 'Automatic' : 'Schedule';
        var scheduleDate = schedule == 'Automatic' ? '' : $('.schedule .calendar').val();
        var categories = JSON.parse('{ "Categories" : [] }');
        var blogKey = $('input[id$="hdfBlogKey"]').val();
        $('.categories input[type="checkbox"]:checked').each(function () {
            categories['Categories'].push({ "CategoryID": $(this).parent().attr('categoryid'), "CategoryName":$(this).next().text()});
        });
        var isPrivate = $('.privacy input[type="checkbox"]').is(':checked');
        var blogParam = { BlogParam : { BlogKey: blogKey, BlogName: postTitle, BlogContent: postContent,BlogSchedule:schedule,BlogScheduleDateString:scheduleDate,IsPrivate:isPrivate,BlogStatus:type, Categories: categories['Categories'] } };
        utilities.ajaxPost('../Management/PostBlog.aspx/PostBlog', JSON.stringify(blogParam), function () { utilities.displayLoadingText('Saving...',false); blog.disabledBlogMenus(true); }, function (data) {
            utilities.displayLoadingText('Saved',true);
            blog.disabledBlogMenus(false);
            if (data.d.Status && !isPreview) {
                window.location = 'Blogger.aspx';
            } else if (!isPreview) {
                utilities.dialogMessage(data.d.Message);
                if (data.d.Message.indexOf('Session') > -1) window.location('../Login.aspx');
            }
        }, function () { blog.disabledBlogMenus(false); })
    },
    checkPostBlog: function () {
        var obj = $('input[id$="txtPostTitle"]');
        if (obj.val() == '') {
            utilities.dialogMessage('Please input post title');
            obj.focus();
            return false;
        }
        if (CKEDITOR.instances.txtContent.getData() == '') {
            utilities.dialogMessage('Please input blog content');            
            return false;
        }
        return true;
    },
    displayBlogList: function (hash, type, keyword) {        
        if ($('.myblog-bar').length > 0) {
            var randomKey = Math.random();
            if (hash.indexOf('comment') < 0) {
                $('.post-group').show();
                $('.comment-group').hide();
                $.ajax({
                    type: 'get',
                    url: 'DisplayBlog.aspx?BlogStatus=' + type + '&SearchContent=' + keyword + '&rnd=' + randomKey,
                    contentType: 'html',
                    cache: false,
                    beforeSend: function () {
                        $('.management-panel-right').html('');
                        utilities.displayLoader();
                    },
                    success: function (response) {
                        utilities.removeLoader();
                        $('input[id$="chkSelectAll"]').prop('checked', false);
                        $('.management-panel-right').html($(response).find('.display-blog-wrapper').html());
                        $('.grid-display-blog input[type="checkbox"]').on('click', function (e) { return false; });

                        $('.grid-display-blog tr').on('click', function () {
                            $(this).toggleClass('row-active');
                            var chkSelect = $(this).find('input[type="checkbox"]').first();
                            chkSelect.prop('checked', !chkSelect.is(':checked'));
                        }).on('mouseenter', function () {
                            $('.visible-on-select').hide();
                            $(this).find('.visible-on-select').show();
                        });

                        $('.grid-display-blog').on('mouseleave', function () { $('.visible-on-select').hide(); });
                        $('.delete-blog').on('click', function () {
                            blog.checkManagementSessionExpired();
                            var blogKeys = { BlogKeys: $(this).attr('key') };
                            utilities.dialogConfirm('Confirm to delete', 'Are you sure you do want to delete selected post?', 'Delete', function () {
                                utilities.ajaxPost('../Management/Blogger.aspx/DeleteBlog', JSON.stringify(blogKeys), function () {
                                    utilities.displayLoadingText('Deleting...', false);
                                }, function (data) {
                                    utilities.displayLoadingText('Deleted.', true);
                                    blog.displayManagementMenus();
                                    blog.displayBlogList(blog.getFullHash(),blog.getHash(), '');
                                }, function () {
                                    utilities.displayLoadingText('Failed.', true);
                                });
                            });
                        });
                        blog.displayManagementMenus();
                    }
                });
            } else {
                $('.post-group').hide();
                $('.comment-group').show();
                $.ajax({
                    type: 'get',
                    url: 'DisplayComment.aspx?CommentType=' + type + '&SearchContent=' + keyword + '&rnd=' + randomKey,
                    contentType: 'html',
                    cache: false,
                    beforeSend: function () {
                        $('.management-panel-right').html('');
                        utilities.displayLoader();
                    }, success: function (response) {
                        utilities.removeLoader();
                        $('input[id$="chkSelectAll"]').prop('checked', false);
                        $('.management-panel-right').html($(response).find('.display-comment-wrapper').html());
                        $('.grid-display-comment input[type="checkbox"]').on('click', function (e) { return false; });

                        $('.grid-display-comment tr').on('click', function () {
                            $(this).toggleClass('row-active');
                            var chkSelect = $(this).find('input[type="checkbox"]').first();
                            chkSelect.prop('checked', !chkSelect.is(':checked'));
                        }).on('mouseenter', function () {
                            $('.visible-on-select').hide();
                            $(this).find('.visible-on-select').show();
                        });

                        $('.grid-display-comment').on('mouseleave', function () { $('.visible-on-select').hide(); });
                        $('.delete-comment').on('click', function () {
                            var commentID = { CommentID: $(this).parent().attr('key') };
                            utilities.dialogConfirm('Confirm to delete', 'Are you sure you do want to delete selected comment?', 'Delete', function () {
                                utilities.ajaxPost('../Default.aspx/DeleteComment', JSON.stringify(commentID), function () {
                                    utilities.displayLoadingText('Deleting...', false);
                                }, function (data) {
                                    utilities.displayLoadingText('Deleted.', true);
                                    blog.displayManagementMenus();
                                    blog.displayBlogList(blog.getFullHash(),blog.getHash(), '');
                                }, function () {
                                    utilities.displayLoadingText('Failed.', true);
                                });
                            });
                        });

                        $('.set-spam,.set-publish').on('click', function () {                            
                            var key = parseInt($(this).parent().attr('key'));
                            var commentType = $(this).attr('class').replace('set-', '');
                            var text = commentType == 'spam' ? 'Spam' : 'Published';
                            var updateStatus = {CommentID:key,CommentType:commentType};
                            utilities.ajaxPost('../Default.aspx/SetCommentType', JSON.stringify(updateStatus), function () {
                                utilities.displayLoadingText('Set to ' + text + '...', false);
                            }, function (data) {
                                utilities.displayLoadingText(text + '.', true);
                                blog.displayManagementMenus();
                                blog.displayBlogList(blog.getFullHash(), blog.getHash(), '');
                            }, function () {
                                utilities.displayLoadingText('Failed.', true);
                            });
                            return false;
                        });                        

                        blog.displayManagementMenus();
                    }
                });
            }
        }
    },
    disabledBlogMenus:function(value){
        $('.blog-menus .button').prop('disabled', value);
    }, displayManagementMenus: function () {
        utilities.ajaxPost('../management/Blogger.aspx/GenerateManagementBlogMenus', '{}', null, function (data) {
            $('.management-panel-left').html(data.d);
            blog.anchorOnLoad();
            $('a[href^="#"]').on('click', function () {
                blog.bindAnchor(this);
            });
        }, null);
    },
    deleteAllCheck: function () {
        blog.checkManagementSessionExpired();
        if ($('.grid-display-blog input[type="checkbox"]:checked').length > 0) {            
            var blogStr = '';
            $('.grid-display-blog input[type="checkbox"]:checked').each(function () {
                if (blogStr != '') blogStr += ',';
                blogStr += $(this).parent().attr('key');
            });
            var blogKeys = { BlogKeys: blogStr };
            utilities.dialogConfirm('Confirm to delete', 'Are you sure you do want to delete selected(s) post?', 'Delete', function () {
                utilities.ajaxPost('../Management/Blogger.aspx/DeleteBlog', JSON.stringify(blogKeys), function () {
                    utilities.displayLoadingText('Deleting...', false);
                }, function (data) {
                    utilities.displayLoadingText('Deleted.', true);
                    blog.displayManagementMenus();
                    blog.displayBlogList(blog.getFullHash(),blog.getHash(),'');
                }, function () {
                    utilities.displayLoadingText('Failed.', true);
                });
            });
        }
    },
    updateCheckStatus: function (status) {
        if ($('.grid-display-blog input[type="checkbox"]:checked').length > 0) {
            var blogStr = '';
            $('.grid-display-blog input[type="checkbox"]:checked').each(function () {
                if (blogStr != '') blogStr += ',';
                blogStr += $(this).parent().attr('key');
            });
            var blogKeys = { BlogKeys: blogStr,Status: status };
            utilities.dialogConfirm('Confirm to update', 'Are you sure you do want to update selected(s) post?', 'Update', function () {
                utilities.ajaxPost('../Management/Blogger.aspx/UpdateStatus', JSON.stringify(blogKeys), function () {
                    utilities.displayLoadingText('Updating...', false);
                }, function (data) {
                    utilities.displayLoadingText('Updated.', true);
                    blog.displayManagementMenus();
                    blog.displayBlogList(blog.getFullHash(),blog.getHash(),'');
                }, function () {
                    utilities.displayLoadingText('Failed.', true);
                });
            });
        }
    },
    updateCheckPrivate: function (status) {
        if ($('.grid-display-blog input[type="checkbox"]:checked').length > 0) {
            var blogStr = '';
            $('.grid-display-blog input[type="checkbox"]:checked').each(function () {
                if (blogStr != '') blogStr += ',';
                blogStr += $(this).parent().attr('key');
            });
            var blogKeys = { BlogKeys: blogStr, Status: status };
            utilities.dialogConfirm('Confirm to update', 'Are you sure you do want to update selected(s) post?', 'Update', function () {
                utilities.ajaxPost('../Management/Blogger.aspx/UpdatePrivate', JSON.stringify(blogKeys), function () {
                    utilities.displayLoadingText('Updating...', false);
                }, function (data) {
                    utilities.displayLoadingText('Updated.', true);
                    blog.displayManagementMenus();
                    blog.displayBlogList(blog.getFullHash(), blog.getHash(), '');
                }, function () {
                    utilities.displayLoadingText('Failed.', true);
                });
            });
        }
    },
    postComment: function (obj) {
        var key = $(obj).attr('key');
        var comment = $(obj).parent().find('textarea');
        if ($.trim(comment.val()) == '') return;
        utilities.ajaxPost('../Default.aspx/PostComment', JSON.stringify({ BlogKey: key, Comment: comment.val() }), function () {            
            $(obj).val('Submit...').prop('disabled', true).addClass('disabled-button');
            comment.addClass('comment-sending').val('Sending... ' + comment.val()).prop('readonly', true);
        }, function (response) {
            if (response.d.indexOf('Session') == 0) window.location = 'http://www.gmtour.com/2Box/Message/Session-Expire';
            $(obj).val('Submit').prop('disabled', false).removeClass('disabled-button');
            blog.displayComment(function () {
                var body = $("html, body");
                body.scrollTop(0);
                body.animate({ scrollTop: $(document).height() }, 1500);
                comment.val('').prop('readonly', false).removeClass('comment-sending');
                $('.comment-submit-box').fadeIn('slow');
                utilities.checkScrollbar();
            });
        }, null);
    },
    displayComment: function (callback) {
        $('.comment-submit-box').fadeOut('fast');
        $('.display-comment').each(function () {
            var key = $(this).attr('key');
            var randomKey = Math.random();
            var obj = $(this);
            $.ajax({
                type: 'get',
                url: '../DisplayComment.aspx?key=' + key + '&rnd=' + randomKey,
                contentType: 'html',
                cache: false,
                beforeSend: function () {
                    obj.html('<div class="comment-loading"><img src="../images/loader.gif" /></div>');
                },
                success: function (data) {
                    $('.comment-loading').remove();
                    obj.html($(data).find('.display-comment-wrapper').html());

                    $('div[this_spam="true"]').addClass('spam-box');

                    var body = $("html, body");
                    body.scrollTop(100);
                    body.scrollTop(0);
                    if (callback != null) callback();
                    blog.displayManagementBox();
                }
            });
        });
    },
    displayManagementBox: function () {
        $('.manage-box').each(function () {
            var obj = $(this);
            var commentID = parseInt(obj.attr('key'));
            utilities.ajaxPost('../Default.aspx/ManagementButton', JSON.stringify({ CommentID: commentID }), null, function (response) {
                obj.html(response.d);
                if (response.d != '') blog.bindManagementBox(obj);
                blog.setSpamBoxCss();
            }, null);
        });
    },
    bindManagementBox: function (parent) {
        $(parent).find('.set-spam,.set-publish').on('click', function () {            
            blog.checkBlogSessionExpired();
            $(parent).fadeOut('fast');
            var commentType = $(this).attr('class').replace('set-', '');
            var key = parseInt($(parent).attr('key'));            
            utilities.ajaxPost('../Default.aspx/SetCommentType', JSON.stringify({ CommentID: key, CommentType: commentType }), function () {
            }, function (response) {
                if (response.d) {
                    utilities.ajaxPost('../Default.aspx/ManagementButton', JSON.stringify({ CommentID: key }), null, function (response) {
                        $(parent).html(response.d);                        
                        if (response.d != '') blog.bindManagementBox(parent);
                        if (response.d.indexOf('set-publish') > -1)
                            $(parent).parent().addClass('spam-box');
                        else
                            $(parent).parent().removeClass('spam-box');
                        $(parent).fadeIn('fast');
                    }, null);
                }
            }, null);
        });
        blog.bindDeleteComment(parent);
    },
    setSpamBoxCss: function () {        
        $('.set-publish').parent().parent().addClass('spam-box');
    },
    bindDeleteComment: function (parent) {        
        $(parent).find('.set-delete').on('click', function () {
            blog.checkBlogSessionExpired();
            var key = parseInt($(parent).attr('key'));          
            utilities.dialogConfirm('Confirm to delete', 'Are you sure to delete selected comment?', 'Delete', function () {
                $(parent).hide('slide', { direction: 'top' }, 500);
                utilities.ajaxPost('../Default.aspx/DeleteComment', JSON.stringify({ CommentID: key }), null, function (response) {
                    if (response.d) {                        
                        $(parent).parent().fadeOut('slow', function () { $(parent).parent().remove(); });
                    }
                }, null);
            });
        });
    },
    deleteComment: function () {
        blog.checkManagementSessionExpired();
        var commentStr = '';
        $('.grid-display-comment input[type="checkbox"]:checked').each(function () {
            if (commentStr != '') commentStr += ',';
            commentStr += $(this).parent().attr('key');
        });
        if (commentStr == '') return;
        var commentIDs = { CommentIDs : commentStr };
        utilities.dialogConfirm('Confirm to delete', 'Are you sure you do want to delete selected(s) comment?', 'Delete', function () {
            utilities.ajaxPost('../Management/Blogger.aspx/DeleteComment', JSON.stringify(commentIDs), function () {
                utilities.displayLoadingText('Deleting...', false);
            }, function (data) {
                utilities.displayLoadingText('Deleted.', true);
                blog.displayManagementMenus();
                blog.displayBlogList(blog.getFullHash(),blog.getHash(), '');
            }, function (response) {
                utilities.displayLoadingText('Failed.', true);
            });
        });
    },
    setCommentType: function (type) {
        var commentStr = '';
        $('.grid-display-comment input[type="checkbox"]:checked').each(function () {
            if (commentStr != '') commentStr += ',';
            commentStr += $(this).parent().attr('key');
        });
        if (commentStr == '') return;
        var commentIDs = { CommentID: commentStr, CommentType: type };
        utilities.dialogConfirm('Confirm to update', 'Are you sure you do want to update selected(s) post?', 'Update', function () {
            utilities.ajaxPost('../Management/Blogger.aspx/setCommentType', JSON.stringify(commentIDs), function () {
                utilities.displayLoadingText('Updating...', false);
            }, function (data) {
                utilities.displayLoadingText('Updated.', true);
                blog.displayManagementMenus();
                blog.displayBlogList(blog.getFullHash(),blog.getHash(), '');
            }, function () {
                utilities.displayLoadingText('Failed.', true);
            });
        });
    },
    checkBlogSessionExpired: function () {
        var response = $.ajax({
            type: 'POST',
            url: '../Default.aspx/SessionExpired',
            data: '{}',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            cache: false,
            async: false
        }).responseText;
        if (JSON.parse(response).d) {
            alert('Session expired.');
            window.location = '../';
        }
    },
    checkManagementSessionExpired: function () {
        var response = $.ajax({
            type: 'POST',
            url: 'Blogger.aspx/SessionExpired',
            data: '{}',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            cache: false,
            async: false
        }).responseText;
        if (JSON.parse(response).d) {
            alert('Session expired.');
            window.location = '../';
        }
    }
}

var utilities = {
    ajaxPost: function (url,data,beforeSend,success,failure) {
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            cache: false,
            beforeSend:beforeSend,
            success: success,
            failure: failure,
            error: failure
        });
    },
    displayLoadBackground: function () {        
        $('<div class="loader-bg"></div>').appendTo('body').fadeIn('fast', function () {
            $('.login-box').fadeIn('medium');
        });
    },
    hideLoadBackground: function () {
        $('.loader-bg').remove();
    },
    dialogMessage: function (message) {
        var options = {
            resizable: false, modal: true, draggable: false, width: 350, height: 200, buttons: {
                Ok: function () {
                    $(this).dialog("close");
                }
            }
        };
        $('.dialog-message .dialog-message-text').text(message).parent().dialog(options);
    },
    dialogConfirm: function (title, text, button_text, button_callback) {
        var options = {
            resizable: false, modal: true, draggable: false, width: 350, height: 200, title: title, buttons: {
                'Ok': function () {
                    $(this).dialog("close");
                    button_callback();
                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            }, open: function () {
                $('.ui-button-text:eq(1)').text(button_text);
            }
        };
        $('.dialog-confirm .dialog-confirm-text').text(text).parent().dialog(options);
    },
    displayLoadingText: function (message,fadeout) {
        if($('.loading-text').length > 0) $('.loading-text').remove();
        $('body').append('<span class="loading-text">' + message + '</span>');
        setTimeout(function () {
            $('.loading-text').fadeOut('slow', function () {
                $('.loading-text').remove();
            });
        }, 3000);
    },
    removeLoadingText: function () {

    },
    displayLoader: function () {
        $('.myblog-bar').append('<img src="../images/loader.gif" class="loader" />');
    },
    removeLoader: function () {
        $('.loader').remove();
    },
    countTextAreaLine: function textareaCurLineNum(obj) {
        if (!/Opera/.test(navigator.userAgent)) {
            return obj.value.split(/\n/g).length;
        } else {
            return obj.value.split(/\n/g).length - 1;
        }
    },
    checkScrollbar: function () {
        if ($('body,html').prop('scrollHeight') > $(window).height())
            $('body').css('overflow-y', 'scroll');
    }
}