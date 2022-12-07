using AutoMapper;
using TuyenDung.Data.Entities;

namespace TuyenDung.Model.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Command

            CreateMap<CommandModel, Command>();

            CreateMap<Command, CommandModel>();

            #endregion Command

            #region ActivityLog

            CreateMap<ActivityLogModel, ActivityLog>();

            CreateMap<ActivityLog, ActivityLogModel>();

            #endregion ActivityLog

            #region Attachment

            CreateMap<AttachmentModel, Attachment>();

            CreateMap<Attachment, AttachmentModel>();

            #endregion Attachment

            #region Category

            CreateMap<CategoryModel, Category>();

            CreateMap<Category, CategoryModel>();

            #endregion Category

            #region CommandInFunction

            CreateMap<CommandInFunctionModel, CommandInFunction>();

            CreateMap<CommandInFunction, CommandInFunctionModel>();

            #endregion CommandInFunction

            #region Comment

            CreateMap<CommentModel, Comment>();

            CreateMap<Comment, CommentModel>();

            #endregion Comment

            #region Function

            CreateMap<FunctionModel, Function>();

            CreateMap<Function, FunctionModel>();

            #endregion Function

            #region KnowledgeBasis

            CreateMap<KnowledgeBasisModel, KnowledgeBasis>();

            CreateMap<KnowledgeBasis, KnowledgeBasisModel>();

            #endregion KnowledgeBasis

            #region Permission

            CreateMap<PermissionModel, Permission>();

            CreateMap<Permission, PermissionModel>();

            #endregion Permission

            #region Vote

            CreateMap<VoteModel, Vote>();

            CreateMap<Vote, VoteModel>();

            #endregion Vote
        }
    }
}