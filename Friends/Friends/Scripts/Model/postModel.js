﻿window.friends.Model.Post = Backbone.Model.extend({
    renderView:function($container) {
        this.view = new window.friends.Views.BasePostView({ model: this, $container: $container });
    },
    relations: {
        comments: window.friends.Collection.Comment,
    },
    parse:function(model) {
        if (!model.comments) model.comments = [];
        return model;
    },
    like:function() {
        
    },
    dislike:function() {
        
    }
});

window.friends.Model.TextPost = friends.Model.Post.extend({
    defaults: {
        postType: 'PostText'
    },
    renderView: function ($container) {
        this.view = new window.friends.Views.TextPostView({ model: this, $container: $container });
    },
    parse: function (result) {
        return result.items;
    },
    url:'/api/textpost'
});

window.friends.Collection.TextPost = Backbone.Collection.extend({
    model: friends.Model.TextPost,

});

window.friends.Collection.Post = Backbone.Collection.extend({
    model: function (m, options) {
        var returnModel;
        switch (m.postType.toLowerCase()) {
            case 'posttext':
                returnModel = new friends.Model.TextPost(m, options);
                break;
            default:
                returnModel = new friends.Model.Post(m, options);
                break;
        }
        return returnModel;
    },
    url:'/api/post',
    parse:function(result) {
        return result.items;
    }
})