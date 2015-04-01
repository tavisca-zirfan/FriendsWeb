﻿window.friends.Views.BasePostView = Backbone.View.extend({
    initialize: function(param) {
        this.options = $.extend({}, this.options, this.childOptions);
        if (!window.friends.hbTemplate.BasePostView) window.friends.hbTemplate.BasePostView = Handlebars.compile($(this.options.baseTemplate).html());
        if (!window.friends.hbTemplate.ChildPostView) window.friends.hbTemplate.ChildPostView = Handlebars.compile($(this.options.childTemplate).html());
        this.$container = param.$container;
        this.render();

    },
    options: {
        baseTemplate: '#basePostViewTemplate',

    },
    render: function() {
        var that = this;
        this.$card = $(window.friends.hbTemplate.BasePostView(this.model));
        $('.post-type-container', this.$card).html($(window.friends.hbTemplate.ChildPostView(this.model)));
        if (this.model.attributes.comments) {
            var comments = this.model.get('comments');
            _.forEach(comments.models, function(comment) {
                that._renderComment(comment);
            });
        }
        this.$container.append(this.$card);
    },
    _renderComment:function(comment) {
        var commentView = new friends.Views.CommentView({ model: comment, $container: $('.comments', this.$card) });
    },
    _renderChild: function() {

    },
    _bindEvents: function () {
        var that = this;
        $('.comment-box', this.$card).on('keydown', function() {
            var comment = new friends.Model.Comment();
            comment.set('commentMessage', $(this).val());
            comment.set('forPostId', that.model.id);
            comment.save({
                success: function(c) {
                    that._renderComment(c);
                }
            });
        });
    },
    _bindChildEvents: function() {},

    _initializeChild: function() {},

});

window.friends.Views.TextPostView = window.friends.Views.BasePostView.extend({
    childOptions: {
        childTemplate: '#textPostViewTemplate'
    }
});