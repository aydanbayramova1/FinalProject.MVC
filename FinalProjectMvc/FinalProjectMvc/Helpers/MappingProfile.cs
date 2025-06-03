using AutoMapper;
using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.AboutBanner;
using FinalProjectMvc.ViewModels.Admin.AboutRestaurant;
using FinalProjectMvc.ViewModels.Admin.Blog;
using FinalProjectMvc.ViewModels.Admin.BlogBanner;
using FinalProjectMvc.ViewModels.Admin.BlogDetailBanner;
using FinalProjectMvc.ViewModels.Admin.Catalog;
using FinalProjectMvc.ViewModels.Admin.ContactBanner;
using FinalProjectMvc.ViewModels.Admin.FaqsBanner;
using FinalProjectMvc.ViewModels.Admin.MenuBanner;
using FinalProjectMvc.ViewModels.Admin.ReservationBanner;
using FinalProjectMvc.ViewModels.Admin.Scrolling;
using FinalProjectMvc.ViewModels.Admin.Service;
using FinalProjectMvc.ViewModels.Admin.ServiceItem;
using FinalProjectMvc.ViewModels.Admin.Slider;
using FinalProjectMvc.ViewModels.Admin.TeamBanner;
using FinalProjectMvc.ViewModels.Admin.TeamMember;
using FinalProjectMvc.ViewModels.Admin.Topbar;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FinalProjectMvc.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SliderCreateVM, Slider>();
            CreateMap<Slider, SliderVM>();
            CreateMap<Slider, SliderDetailVM>();

            CreateMap<Scrolling, ScrollingVM>().ReverseMap();
            CreateMap<Scrolling, ScrollingDetailVM>().ReverseMap();
            CreateMap<ScrollingCreateVM, Scrolling>();

            CreateMap<Catalog, CatalogVM>().ReverseMap();
            CreateMap<Catalog, CatalogDetailVM>().ReverseMap();
            CreateMap<CatalogCreateVM, Catalog>();
            CreateMap<CatalogEditVM, Catalog>().ReverseMap();
            CreateMap<Service, ServiceCreateVM>().ReverseMap();
            CreateMap<Service, ServiceEditVM>().ReverseMap();
            CreateMap<Service, ServiceDetailVM>();

            CreateMap<Service, ServiceVM>();
            CreateMap<ServiceItem, ServiceItemVM>();
            CreateMap<ServiceCreateVM, Service>();
            CreateMap<ServiceEditVM, Service>();
            CreateMap<Service, ServiceEditVM>();
            CreateMap<Service, ServiceDetailVM>();

            CreateMap<ServiceItem, ServiceItemVM>()
            .ForMember(dest => dest.ServiceTitle, opt => opt.MapFrom(src => src.Service.Title));
            CreateMap<ServiceItem, ServiceItemEditVM>().ReverseMap();
            CreateMap<ServiceItem, ServiceItemDetailVM>()
           .ForMember(dest => dest.ServiceTitle, opt => opt.MapFrom(src => src.Service.Title));


            CreateMap<ServiceItemCreateVM, ServiceItem>();

            CreateMap<Topbar, TopbarVM>();
            CreateMap<TopbarCreateVM, Topbar>();
            CreateMap<Topbar, TopbarDetailVM>(); 
            CreateMap<Topbar, TopbarEditVM>();
            CreateMap<TopbarEditVM, Topbar>();


            CreateMap<AboutBannerCreateVM, AboutBanner>()
            .ForMember(dest => dest.Img, opt => opt.Ignore()); 

            
            CreateMap<AboutBanner, AboutBannerEditVM>().ReverseMap()
                .ForMember(dest => dest.Img, opt => opt.Ignore());       
            CreateMap<AboutBanner, AboutBannerDetailVM>();
            CreateMap<AboutBanner, AboutBannerVM>();

            CreateMap<BlogBannerCreateVM, BlogBanner>()
           .ForMember(dest => dest.Img, opt => opt.Ignore());


            CreateMap<BlogBanner, BlogBannerEditVM>().ReverseMap()
               .ForMember(dest => dest.Img, opt => opt.Ignore());
            CreateMap<BlogBanner, BlogBannerDetailVM>();
            CreateMap<BlogBanner, BlogBannerVM>();


            CreateMap<MenuBanner, MenuBannerEditVM>().ReverseMap()
           .ForMember(dest => dest.Img, opt => opt.Ignore());

            CreateMap<MenuBanner, MenuBannerDetailVM>();
            CreateMap<MenuBanner, MenuBannerVM>();

            CreateMap<TeamBanner, TeamBannerEditVM>().ReverseMap()
           .ForMember(dest => dest.Img, opt => opt.Ignore());
            CreateMap<TeamBanner, TeamBannerDetailVM>();
            CreateMap<TeamBanner, TeamBannerVM>();


            CreateMap<FaqsBanner, FaqsBannerEditVM>().ReverseMap()
          .ForMember(dest => dest.Img, opt => opt.Ignore());
            CreateMap<FaqsBanner, FaqsBannerDetailVM>();
            CreateMap<FaqsBanner, FaqsBannerVM>();

            CreateMap<ContactBanner, ContactBannerEditVM>().ReverseMap()
          .ForMember(dest => dest.Img, opt => opt.Ignore());
            CreateMap<ContactBanner, ContactBannerDetailVM>();
            CreateMap<ContactBanner, ContactBannerVM>();


            CreateMap<BlogDetailBanner, BlogDetailBannerEditVM>().ReverseMap()
           .ForMember(dest => dest.Img, opt => opt.Ignore());
            CreateMap<BlogDetailBanner, BlogDetailBannerDetailVM>();
            CreateMap<BlogDetailBanner, BlogDetailBannerVM>();


            CreateMap<ReservationBanner, ReservationBannerEditVM>().ReverseMap()
            .ForMember(dest => dest.Img, opt => opt.Ignore());
            CreateMap<ReservationBanner, ReservationBannerDetailVM>();
            CreateMap<ReservationBanner, ReservationBannerVM>();


            CreateMap<TeamMember, TeamMemberCreateVM>().ReverseMap();
            CreateMap<TeamMember, TeamMemberEditVM>().ReverseMap();
            CreateMap<TeamMember, TeamMemberDetailVM>();
            CreateMap<TeamMember, TeamMemberVM>()
           .ForMember(dest => dest.Desc, opt => opt.MapFrom(src => src.Description))
           .ForMember(dest => dest.ImgUrl, opt => opt.MapFrom(src => src.Img));


            CreateMap<Blog, BlogVM>();
            CreateMap<BlogCreateVM, Blog>()
            .ForMember(dest => dest.Img, opt => opt.Ignore())
            .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.Date));
            CreateMap<BlogEditVM, Blog>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreateDate, opt => opt.Ignore());
            CreateMap<Blog, BlogDetailVM>();

            CreateMap<AboutRestaurantCreateVM, AboutRestaurant>()
            .ForMember(dest => dest.Image, opt => opt.Ignore());

            CreateMap<AboutRestaurant, AboutRestaurantEditVM>()
                .ForMember(dest => dest.ImageFile, opt => opt.Ignore());

            CreateMap<AboutRestaurantEditVM, AboutRestaurant>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());
            CreateMap<AboutRestaurant, AboutRestaurantDetailVM>();
            CreateMap<AboutRestaurant, AboutRestaurantVM>();


        }
    }
}