using AutoMapper;
using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.AboutBanner;
using FinalProjectMvc.ViewModels.Admin.AboutRestaurant;
using FinalProjectMvc.ViewModels.Admin.AboutUs;
using FinalProjectMvc.ViewModels.Admin.AboutUsItem;
using FinalProjectMvc.ViewModels.Admin.Approach;
using FinalProjectMvc.ViewModels.Admin.ApproachItem;
using FinalProjectMvc.ViewModels.Admin.Blog;
using FinalProjectMvc.ViewModels.Admin.BlogBanner;
using FinalProjectMvc.ViewModels.Admin.BlogDetailBanner;
using FinalProjectMvc.ViewModels.Admin.Catalog;
using FinalProjectMvc.ViewModels.Admin.ContactBanner;
using FinalProjectMvc.ViewModels.Admin.ContactMessage;
using FinalProjectMvc.ViewModels.Admin.ContactUs;
using FinalProjectMvc.ViewModels.Admin.FaqsBanner;
using FinalProjectMvc.ViewModels.Admin.IntroCounter;
using FinalProjectMvc.ViewModels.Admin.MenuBanner;
using FinalProjectMvc.ViewModels.Admin.OpeningHour;
using FinalProjectMvc.ViewModels.Admin.OurStory;
using FinalProjectMvc.ViewModels.Admin.ReservationBanner;
using FinalProjectMvc.ViewModels.Admin.Scrolling;
using FinalProjectMvc.ViewModels.Admin.Service;
using FinalProjectMvc.ViewModels.Admin.ServiceItem;
using FinalProjectMvc.ViewModels.Admin.Setting;
using FinalProjectMvc.ViewModels.Admin.Slider;
using FinalProjectMvc.ViewModels.Admin.StoryItem;
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

            CreateMap<Approach, ApproachVM>().ReverseMap();
            CreateMap<Approach, ApproachCreateVM>().ReverseMap();
            CreateMap<Approach, ApproachEditVM>().ReverseMap();
            CreateMap<Approach, ApproachDetailVM>().ReverseMap();
            CreateMap<ApproachCreateVM, Approach>()
            .ForMember(dest => dest.Image, opt => opt.Ignore());

            CreateMap<ApproachItemCreateVM, ApproachItem>()
                .ForMember(dest => dest.IconPath, opt => opt.Ignore()); 

            CreateMap<ApproachItem, ApproachItemVM>()
                .ForMember(dest => dest.IconImg, opt => opt.MapFrom(src => src.IconPath));

            CreateMap<ApproachItem, ApproachItemDetailVM>()
                .ForMember(dest => dest.IconImg, opt => opt.MapFrom(src => src.IconPath));

            CreateMap<ApproachItem, ApproachItemEditVM>()
                .ForMember(dest => dest.IconImg, opt => opt.MapFrom(src => src.IconPath));
            CreateMap<ApproachItemEditVM, ApproachItem>()
                .ForMember(dest => dest.IconPath, opt => opt.Ignore()) 
                .ForMember(dest => dest.Approach, opt => opt.Ignore()); 

            CreateMap<SettingCreateVM, Setting>();
            CreateMap<SettingEditVM, Setting>();
            CreateMap<Setting, SettingEditVM>();
            CreateMap<Setting, SettingCreateVM>();



            CreateMap<ContactUs, ContactUsVM>().ReverseMap();
            CreateMap<ContactUs, ContactUsCreateVM>().ReverseMap();
            CreateMap<ContactUs, ContactUsEditVM>().ReverseMap();
            CreateMap<ContactUs, ContactUsDetailVM>().ReverseMap();


            CreateMap<ContactMessageCreateVM, ContactMessage>()
                        .ForMember(dest => dest.CreateDate, opt => opt.Ignore());

            CreateMap<ContactMessage, ContactMessageVM>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreateDate));


            CreateMap<OurStory, OurStoryVM>();
            CreateMap<OurStory, OurStoryDetailVM>();
            CreateMap<OurStoryCreateVM, OurStory>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());
            CreateMap<OurStoryEditVM, OurStory>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());
            CreateMap<OurStory, OurStoryEditVM>();


            CreateMap<StoryItem, StoryItemVM>();
            CreateMap<StoryItem, StoryItemDetailVM>();

            CreateMap<StoryItemCreateVM, StoryItem>()
                .ForMember(dest => dest.IconImg, opt => opt.Ignore()); 

            CreateMap<StoryItemEditVM, StoryItem>()
                .ForMember(dest => dest.IconImg, opt => opt.Ignore());

            CreateMap<StoryItem, StoryItemEditVM>();

            CreateMap<IntroCounter, IntroCounterVM>();
            CreateMap<IntroCounterVM, IntroCounter>();

            CreateMap<IntroCounterCreateVM, IntroCounter>();
            CreateMap<IntroCounter, IntroCounterCreateVM>();

            CreateMap<IntroCounter, IntroCounterEditVM>();
            CreateMap<IntroCounterEditVM, IntroCounter>();


            CreateMap<AboutUs, AboutUsVM>().ReverseMap();
            CreateMap<AboutUs, AboutUsCreateVM>().ReverseMap();
            CreateMap<AboutUs, AboutUsEditVM>().ReverseMap();
            CreateMap<AboutUs, AboutUsDetailVM>().ReverseMap();
            CreateMap<AboutUs, AboutUsDetailVM>()
    .ForMember(dest => dest.AboutUsItems, opt => opt.MapFrom(src => src.AboutUsItems))
    .ForMember(dest => dest.OpeningHours, opt => opt.MapFrom(src => src.OpeningHours));



            CreateMap<OpeningHour, OpeningHourVM>().ReverseMap();
            CreateMap<OpeningHour, OpeningHourDetailVM>().ReverseMap();
            CreateMap<OpeningHour, OpeningHourEditVM>().ReverseMap();
            CreateMap<OpeningHour, OpeningHourCreateVM>().ReverseMap();
            CreateMap<AboutUsItem, AboutUsItemVM>();
            CreateMap<AboutUsItem, AboutUsItemDetailVM>()
     .ForMember(dest => dest.IconPath, opt => opt.MapFrom(src => src.IconUrl));
            CreateMap<AboutUsItem, AboutUsItemEditVM>().ReverseMap();
            CreateMap<AboutUsItemCreateVM, AboutUsItem>();

            CreateMap<AboutUsEditVM, AboutUs>()
    .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
    .ForMember(dest => dest.VideoUrl, opt => opt.Ignore());

            CreateMap<AboutUs, AboutUsVM>()

    .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));
            CreateMap<AboutUs, AboutUsVM>();
            CreateMap<OpeningHour, OpeningHourVM>();


        }
    }
}