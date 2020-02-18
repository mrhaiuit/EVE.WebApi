#region

using AutoMapper;
using EVE.ApiModels.Authentication.Request;
using EVE.ApiModels.Catalog;
using EVE.Data;

#endregion

namespace EVE.WebApi.Authentication.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<EduDepartmentInsertReq, EduDepartment>().ReverseMap();
            CreateMap<EduDepartmentUpdateReq, EduDepartment>().ReverseMap();
            CreateMap<EduProvinceInsertReq, EduProvince>().ReverseMap();
            CreateMap<EduProvinceUpdateReq, EduProvince>().ReverseMap();
            CreateMap<EmployeeInsertReq, Employee>().ReverseMap();
            CreateMap<EmployeeUpdateReq, Employee>().ReverseMap();
            CreateMap<EvalCriteriaInsertReq, EvalCriteria>().ReverseMap();
            CreateMap<EvalCriteriaUpdateReq, EvalCriteria>().ReverseMap();
            CreateMap<EvalDetailInsertReq, EvalDetail>().ReverseMap();
            CreateMap<EvalDetailUpdateReq, EvalDetail>().ReverseMap();
            CreateMap<EvalGuideInsertReq, EvalGuide>().ReverseMap();
            CreateMap<EvalGuideUpdateReq, EvalGuide>().ReverseMap();
            CreateMap<EvalMasterInsertReq, EvalMaster>().ReverseMap();
            CreateMap<EvalMasterUpdateReq, EvalMaster>().ReverseMap();
            CreateMap<EvalPeriodInsertReq, EvalPeriod>().ReverseMap();
            CreateMap<EvalPeriodUpdateReq, EvalPeriod>().ReverseMap();
            CreateMap<EvalResultInsertReq, EvalResult>().ReverseMap();
            CreateMap<EvalResultUpdateReq, EvalResult>().ReverseMap();
            CreateMap<EvalStandardInsertReq, EvalStandard>().ReverseMap();
            CreateMap<EvalStandardUpdateReq, EvalStandard>().ReverseMap();
            CreateMap<EvalStateInsertReq, EvalState>().ReverseMap();
            CreateMap<EvalStateUpdateReq, EvalState>().ReverseMap();

            //CreateMap<EvalTypeInsertReq, EvalType>()
            //        .ReverseMap();
            //CreateMap<EvalTypeUpdateReq, EvalType>()
            //        .ReverseMap();

            CreateMap<FormGroupInsertReq, FormGroup>().ReverseMap();
            CreateMap<FormGroupUpdateReq, FormGroup>().ReverseMap();
            CreateMap<FormsInsertReq, Form>().ReverseMap();
            CreateMap<FormsUpdateReq, Form>().ReverseMap();
            CreateMap<LoginUserInsertReq, LoginUser>().ReverseMap();
            CreateMap<LoginUserUpdateReq, LoginUser>().ReverseMap();
            CreateMap<SchoolInsertReq, School>().ReverseMap();
            CreateMap<SchoolUpdateReq, School>().ReverseMap();
            CreateMap<SchoolLevelInsertReq, SchoolLevel>().ReverseMap();
            CreateMap<SchoolLevelUpdateReq, SchoolLevel>().ReverseMap();
            CreateMap<UserGroupInsertReq, UserGroup>().ReverseMap();
            CreateMap<UserGroupUpdateReq, UserGroup>().ReverseMap();
            CreateMap<UserGroupEmployeeInsertReq, UserGroup_Employee>().ReverseMap();
            CreateMap<UserGroupEmployeeUpdateReq, UserGroup_Employee>().ReverseMap();
            CreateMap<UserGroupFormInsertReq, UserGroup_Form>().ReverseMap();
            CreateMap<UserGroupFormUpdateReq, UserGroup_Form>().ReverseMap();
        }
    }
}
