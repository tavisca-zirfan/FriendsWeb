window.friends.Model.Comment = Backbone.Model.extend({

});

window.friends.Collection.Comment = Backbone.Collection.extend({
    model: friends.Model.Comment,
    parse:function(results) {
        return results.items;
    },
    url:'/api/comment'
})