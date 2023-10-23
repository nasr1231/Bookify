namespace Bookify.Core.Mapping
{
    public class MappingProfile : Profile
    {
        public void MapProfile()
        {
            CreateMap<Category, CategoryViewModel>();
        }
    }
}
