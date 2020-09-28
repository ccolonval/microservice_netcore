namespace myApi.Profiles
{
    public class PersonProfile : AutoMapper.Profile
    {
        public PersonProfile()
        {
            CreateMap<myData.DTO.Person, Models.Person>();
        }
    }
}
