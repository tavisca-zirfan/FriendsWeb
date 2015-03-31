window.friends.Views.CommentView = Backbone.View.extend({
    initialize: function(param) {
        this.options= $.extend({}, this.options,param);
        if (!window.friends.hbTemplate.CommentView) window.friends.hbTemplate.CommentView = Handlebars.compile($(this.template).html());
        this.$container = param.$container;
        this.render();
    },
    template: '#commentViewTemplate',
    options: {

    },
    render: function() {
        this.$container.append($(window.friends.hbTemplate.CommentView(this.model)));
    },
    
    _bindEvents: function() {

    },
    
});