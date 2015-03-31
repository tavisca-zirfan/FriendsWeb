window.friends.Views.BasePostView = Backbone.View.extend({
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
        var $card = $(window.friends.hbTemplate.BasePostView(this.model));
        $('.post-type-container', $card).html($(window.friends.hbTemplate.ChildPostView(this.model)));
        if (this.model.attributes.comments) {
            var comments = this.model.get('comments');
            _.forEach(comments.models, function(comment) {
                var commentView = new friends.Views.CommentView({ model: comment, $container: $('.comments', $card) });
            });
        }
        this.$container.append($card);
    },
    _renderChild: function() {

    },
    _bindEvents: function() {

    },
    _bindChildEvents: function() {},

    _initializeChild: function() {},

});

window.friends.Views.TextPostView = window.friends.Views.BasePostView.extend({
    childOptions: {
        childTemplate: '#textPostViewTemplate'
    }
});