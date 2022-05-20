$(function () {
    "use strict";
    var origin = window.location.origin;
    var comment = { entityId: window.ShopCommentEntity.entityId, entityTypeId: window.ShopCommentEntity.entityTypeId };
    var comments = [];
    var search = '';
    var commentCount = 0;

    // load Comment
     function template(response) {
            var html = ``;
            $.each(response, function (index, item) {
                html += ` <div><div class="commenter-name">`
                if (item.status != 'Approved') {
                    html += `<span class="badge badge-info">${item.status}</span>`
                }
                html += `${item.commenterName}</div><div class="">${item.commentText}</div><div>
                    <span class="r-time">${item.createdOnRelative}</span>
                    - <a data-toggle="collapse" href="#reply${item.id}" class="addReply" role="button" aria-expanded="false" aria-controls="reply${item.id}">Reply</a>
                </div><div class="comment-reply">`
                $.each(item.replies, function (index, childItem) {
                    html += `<div>
                        <div class="commenter-name">`
                    if (childItem.status != 'Approved') {
                        html += `<span class="badge badge-info">${childItem.status}</span>`
                    }

                    html += `${childItem.commenterName}
                        </div>
                        <div class="">${childItem.commentText}</div>
                        <div>
                            <span class="r-time">${childItem.createdOnRelative}</span>
                            - <a data-toggle="collapse" href="#reply${item.id}" class="addReply" role="button" aria-expanded="false" aria-controls="reply${item.id}">Reply</a>
                        </div>
                    </div>`
                })

                html += `<div class="collapse" id="reply${item.id}">`
                if (userAuthorized) {
                    html += `<form name="replyForm" data-id=${item.id}>
                            <div class="form-group">
                                <textarea required maxlength="300" rows="3" class="form-control" name="content-reply" placeholder="Your comment"></textarea>
                            </div>
                            <div class="row">
                                <div class="col-md-9">
                                    <p class="comment-guide-reply">Your comment should not have personal information. 10 - 300 characters : </p>
                                </div>
                                <div class="form-group reply-btn col-md-3 text-right">
                                    <button type="button" class="btn btn-secondary pull-right saveReply"disabled >Add Reply</button>
                                </div>
                            </div>
                        </form>`
                }
                else {
                    html += `<p> Please <a href="/login?ReturnUrl=${ReturnUrl}"> login </a>or<a href="/register?ReturnUrl=${ReturnUrl}"> register </a>to reply.</p>`
                }
                html += `</div></div></div>`
            })

            return html;
        }
     function getComments() {
         var container = $('#pagination');
     
         container.pagination({
             dataSource: `${origin}/comments?entityId=${comment.entityId}&entityTypeId=${comment.entityTypeId}&search=${search}`,
             locator: 'items',
             totalNumberLocator: function (response) {
                 // you can return totalNumber by analyzing response content
                 return response.totalItems;
             },
              pageSize: 10,
               ajax: {
                   beforeSend: function () {
                       $('#comments-list').html('Loading data from shop ...');
                   }
               },
             callback: function (response, pagination) {
                    var html = template(response);
                    $('#comments-list').html(html);
                     contentreply();
                     onload();
             }
            })
    
     
        }
     function contentreply() {
        var contentreply = $("textarea[name='content-reply']");
        $.each(contentreply, function (index, item) {
            $(item).on("keyup", function () {
                var parent = $(item).parents("form[name='replyForm']");
                comment.commentText = contentreply.val();
                $(parent).find(".comment-guide-reply").text(`Your comment should not have personal information. 10 - 300 characters: ${comment.commentText.length}`);
                if ($(this).val().length > 10) {
                    $(parent).find(".saveReply").prop('disabled', false);
                } else {
                    $(parent).find(".saveReply").prop('disabled', true);
                }
            })
        })
    }
     function saveComment() {

         $.post("comments",  comment , function (data) {
            getComments();
             comment.commentText = '';
             var contentText = $("textarea[name='contentText']");
             $(".addcomment").prop('disabled', true);
             contentText.val('');
         }).catch(function (error) {
             toastr.error(error.responseJSON.CommentText);
         });
    };
     getComments();
    var contentText = $("textarea[name='contentText']");
    $(".comment-guide").text(`Your comment should not have personal information. 10 - 300 characters : `);
    $(contentText).on("keyup", function () {
        comment.commentText = contentText.val();
        $(".comment-guide").text(`Your comment should not have personal information. 10 - 300 characters: ${comment.commentText.length}`);
        if ($(this).val().length > 10) {
            $(".addcomment").prop('disabled', false);
        } else {
            $(".addcomment").prop('disabled', true);
        }
        $(".comment-count").val(`${commentCount} Comments`)
    })

    $(".addcomment").on("click", function () {
        saveComment();
    })
    function onload() {
        function addReply() {
            comment.newReply = { entityId: comment.entityId, entityTypeId: comment.entityTypeId };
        };
        $(".addReply").on("click", function () {
            addReply();
        })
        function saveReply(saveReply) {
            var parent = $(saveReply).parents("form[name='replyForm']");
            var Id = parseInt(parent.data("id"));
            var Name = parent.find("textarea[name='content-reply']").val();
            comment.newReply.parentId = Id;
            comment.newReply.CommentText = Name;
            $.post("comments", comment.newReply, function (result) {
                getComments();
                comment.newReply.CommentText = '';
            }).catch(function (error) {
                toastr.error(error.responseJSON.CommentText);
            });;
        };
        $(".saveReply").on('click', function () {
            saveReply(this);
        });
    }
    $(".comment-search").on('keyup', function () {
         search = $(this).val();
         getComments();
     });

   
});
