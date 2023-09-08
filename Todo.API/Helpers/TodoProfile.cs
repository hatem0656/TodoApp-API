using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Core.Dtos.Todo;
using Todo.DAL.Models;

namespace Todo.Core.Helpers
{
    public class TodoProfile : Profile
    {
        public TodoProfile() {
            CreateMap<TodoItem, TodoGetResponse>()
            .ForMember(dest => dest.CreatedBy, act => act.MapFrom(src => src.User.UserName));

            CreateMap<TodoAddRequest, TodoItem> ();

            CreateMap<TodoUpdated, TodoItem>();
            CreateMap<TodoItem , TodoUpdated>();

        }
    }
}
