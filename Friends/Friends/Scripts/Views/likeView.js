window.friends.Views.LikeView = Backbone.View.extend({
    template: '#likeViewTemplate',
    initialize:function(params) {
        if (!window.friends.hbTemplate.LikeView) friends.hbTemplate.LikeView = Handlebars.compile($(this.template).html());
        this.listenTo(this.model, 'sync', this.render);
    },
    render:function() {
        var that = this;
        this.$el.html($(friends.hbTemplate.LikeView(this.model)));
        this.bindEvents();
    },
    bindEvents: function () {
        var that = this;
        $('glyphicon-thumbs-up', this.$el).on('click', function() {
            that.model.like();
        });
        $('glyphicon-thumbs-down', this.$el).on('click', function () {
            that.model.dislike();
        });
    }
});