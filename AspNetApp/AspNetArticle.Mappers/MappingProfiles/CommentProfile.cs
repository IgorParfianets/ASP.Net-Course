﻿using AspNetArticle.Api.Models.Request;
using AspNetArticle.Core.DataTransferObjects;
using AspNetArticle.Database.Entities;
using AspNetArticle.MvcApp.Models;
using AutoMapper;

namespace AspNetArticle.MvcApp.MappingProfiles;


public class CommentProfile : Profile
{
    public CommentProfile()
    {
        // For Entity -> Dto & Dto -> Entity
        CreateMap<Comment, CommentDto>()
            .ForMember(dto => dto.Id, opt => opt.MapFrom(comment => comment.Id))
            .ForMember(dto => dto.Description, opt => opt.MapFrom(comment => comment.Description))
            .ForMember(dto => dto.PublicationDate, opt => opt.MapFrom(comment => comment.PublicationDate));

        CreateMap<CommentDto, Comment>()
            .ForMember(comment => comment.Id, opt => opt.MapFrom(dto => dto.Id))
            .ForMember(comment => comment.Description, opt => opt.MapFrom(dto => dto.Description))
            .ForMember(comment => comment.PublicationDate, opt => opt.MapFrom(dto => dto.PublicationDate));

        CreateMap<AddCommentRequestModel, CommentDto>()
            .ForMember(comment => comment.Id,
                opt
                    => opt.MapFrom(dto => Guid.NewGuid()))
            .ForMember(comment => comment.Description,
                opt
                    => opt.MapFrom(dto => dto.Description))
            .ForMember(comment => comment.PublicationDate,
                opt
                    => opt.MapFrom(dto => DateTime.Now))
            .ForMember(comment => comment.ArticleId,
                opt
                    => opt.MapFrom(dto => dto.ArticleId))
            .ForMember(comment => comment.UserId,
                opt
                    => opt.MapFrom(dto => dto.UserId));


        CreateMap<CommentDto, CommentaryModel>()
            .ForMember(comment => comment.Id,
                opt
                    => opt.MapFrom(dto => dto.Id))
            .ForMember(comment => comment.Description,
                opt
                    => opt.MapFrom(dto => dto.Description));

    }
}
