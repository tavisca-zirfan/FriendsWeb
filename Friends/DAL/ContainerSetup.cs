using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessDomain.DomainObjects;
using DAL.Implementation;
using DAL.Interfaces;
using Infrastructure.Configuration;
using Infrastructure.Data;

namespace DAL
{
    public class ContainerSetup:IContainerSetup
    {
        public void Setup(Infrastructure.Container.IDependencyContainer container)
        {
            container
                .Register<IFilterParser<FriendsDb.Models.Post>, PostListBaseFilter>(FilterType.PostBaseFilter.ToString())
                .Register<IFilterParser<FriendsDb.Models.Post>, PostListLastUpdateFilter>(FilterType.PostLastUpdateFilter.ToString())
                .Register<IFilterParser<FriendsDb.Models.Post>, PostListUserFilter>(FilterType.PostListUserFilter.ToString())
                .Register<IPostTypeRepository, PostTextRepository>(PostType.PostText.ToString())
                .Register<IPostTypeRepository, CommentRepository>(PostType.Comment.ToString())
                .RegisterAsSingleton<IUnitOfWork>(new UnitOfWork())
                ;

        }
    }
}
