using AutoMapper;
using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.Catalog;
using FinalProjectMvc.ViewModels.Admin.Scrolling;
using FinalProjectMvc.ViewModels.Admin.Service;
using FinalProjectMvc.ViewModels.Admin.ServiceItem;
using FinalProjectMvc.ViewModels.Admin.Slider;
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

            CreateMap<ServiceItemCreateVM, ServiceItem>();
            CreateMap<ServiceItem, ServiceItemEditVM>();
            CreateMap<ServiceItem, ServiceItemDetailVM>();
        }
    }
}