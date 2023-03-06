using AutoMapper;
using Yuebon.Chaochi.Core.Dtos;
using Yuebon.Chaochi.Core.Models;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class _ChaochiProfile : Profile
    {
        /// <summary>
        /// /
        /// </summary>
        public _ChaochiProfile()
        {

            CreateMap<User, UserOutputDto>().ReverseMap();
            CreateMap<UserInputDto, User>();
            CreateMap<User, Security.Dtos.UserLoginDto>()
                .ForMember(e => e.UserId, s => s.MapFrom(o => o.Id));

            CreateMap<EqRepair, EqRepairOutputDto>();
            CreateMap<EqRepairInputDto, EqRepair>();

            CreateMap<CustomerLN, CustomerLNOutputDto>().ReverseMap();
            CreateMap<CustomerLNInputDto, CustomerLN>();

            CreateMap<CustomerLC, CustomerLCOutputDto>().ReverseMap();
            CreateMap<CustomerLCInputDto, CustomerLC>();

            CreateMap<CustomerRN, CustomerRNOutputDto>().ReverseMap();
            CreateMap<CustomerRNInputDto, CustomerRN>();

            CreateMap<CustomerRC, CustomerRCOutputDto>();
            CreateMap<CustomerRCInputDto, CustomerRC>();

            CreateMap<HistoryFormLN, HistoryFormLNOutputDto>().ReverseMap();

            CreateMap<HistoryFormLC, HistoryFormLCOutputDto>().ReverseMap();

            CreateMap<HistoryFormRC, HistoryFormRCOutputDto>().ReverseMap();

            CreateMap<HistoryFormRN, HistoryFormRNOutputDto>().ReverseMap();

            CreateMap<HistoryFormBuilding, HistoryFormBuildingOutputDto>().ReverseMap();

            CreateMap<Event, EventOutputDto>().ReverseMap(); ;
            CreateMap<Event, EventDashboardOutputDto>().ReverseMap(); ;
            CreateMap<EventInputDto, Event>();

            CreateMap<RemittanceL, RemittanceLOutputDto>();
            CreateMap<RemittanceLInputDto, RemittanceL>();

            CreateMap<RemittanceR, RemittanceROutputDto>();
            CreateMap<RemittanceRInputDto, RemittanceR>();

            CreateMap<EventQuestionnaire, EventQuestionnaireOutputDto>();
            CreateMap<EventQuestionnaireInputDto, EventQuestionnaire>();

            CreateMap<EventQuestionnaireTopic, EventQuestionnaireTopicOutputDto>();
            CreateMap<EventQuestionnaireTopicInputDto, EventQuestionnaireTopic>();

            CreateMap<EventSatisfaction, EventSatisfactionOutputDto>();
            CreateMap<EventSatisfactionInputDto, EventSatisfaction>();

            CreateMap<Satisfaction, SatisfactionOutputDto>();
            CreateMap<SatisfactionInputDto, Satisfaction>();

            CreateMap<SatisfactionTopic, SatisfactionTopicOutputDto>();
            CreateMap<SatisfactionTopicInputDto, SatisfactionTopic>();

            CreateMap<SecurityForm, SecurityFormOutputDto>();
            CreateMap<SecurityFormInputDto, SecurityForm>();

            CreateMap<EqRepairDetail, EqRepairDetailOutputDto>();
            CreateMap<EqRepairDetailInputDto, EqRepairDetail>();

            CreateMap<EventCost, EventCostOutputDto>();
            CreateMap<EventCostInputDto, EventCost>();

            CreateMap<EventPersonnel, EventPersonnelOutputDto>();
            CreateMap<EventPersonnelInputDto, EventPersonnel>();

            CreateMap<Complaint, ComplaintOutputDto>();
            CreateMap<ComplaintInputDto, Complaint>();

            CreateMap<ComplaintNoticeMail, ComplaintNoticeMailOutputDto>();
            CreateMap<ComplaintNoticeMailInputDto, ComplaintNoticeMail>();

            CreateMap<SendMailInfo, SendMailInfoOutputDto>();
            CreateMap<SendMailInfoInputDto, SendMailInfo>();

            CreateMap<BlackList, BlackListOutputDto>();
            CreateMap<BlackListInputDto, BlackList>();

            CreateMap<TBNoB6, TBNoB6OutputDto>();
            CreateMap<TBNoB6InputDto, TBNoB6>();

            CreateMap<EventGuest, EventGuestOutputDto>();
            CreateMap<EventGuestInputDto, EventGuest>();

            CreateMap<Bankinfo, BankinfoOutputDto>();
            CreateMap<BankinfoInputDto, Bankinfo>();

            CreateMap<ContractRemittance, ContractRemittanceOutputDto>();
            CreateMap<ContractRemittanceInputDto, ContractRemittance>();

            CreateMap<AdministrativeWorkLog, AdministrativeWorkLogOutputDto>();
            CreateMap<AdministrativeWorkLogInputDto, AdministrativeWorkLog>();
            CreateMap<AdministrativeWorkLogInputDto_update, AdministrativeWorkLog>();

            CreateMap<AdministrativeWorkLogDetails, AdministrativeWorkLogDetailsOutputDto>();
            CreateMap<AdministrativeWorkLogDetailsInputDto, AdministrativeWorkLogDetails>();

            CreateMap<SalesWorkLog, SalesWorkLogOutputDto>();
            CreateMap<SalesWorkLogInputDto, SalesWorkLog>();

            CreateMap<SalesWorkLogDetails, SalesWorkLogDetailsOutputDto>();
            CreateMap<SalesWorkLogDetailsInputDto, SalesWorkLogDetails>();

            CreateMap<Building, BuildingOutputDto>().ReverseMap();
            CreateMap<BuildingInputDto, Building>();//.IgnoreAllPropertiesWithAnInaccessibleSetteer();

            CreateMap<Building1, Building1OutputDto>().ReverseMap();
            CreateMap<Building1InputDto, Building1>();

            CreateMap<Building, BuildingListOutputDto>().ReverseMap();

            CreateMap<Building, BuildingSituationOutputDto>().ReverseMap();
            CreateMap<BuildingSituationInputDto, Building>();//.IgnoreAllPropertiesWithAnInaccessibleSetteer();

            CreateMap<Building, BuildingRentBasicOutputDto>().ReverseMap();
            CreateMap<BuildingRentBasicInputDto, Building>();//.IgnoreAllPropertiesWithAnInaccessibleSetteer();

            // 建物廣告管理
            CreateMap<BuildingAdvertisement, BuildingAdvertisementOutputDto>().ReverseMap();
            CreateMap<BuildingAdvertisementInputDto, BuildingAdvertisement>();

            CreateMap<CustomerRN, RNFOutputDto>().ReverseMap();
            CreateMap<RNFInputDto, CustomerRN>();//.IgnoreAllPropertiesWithAnInaccessibleSetteer();

            CreateMap<CustomerRC, RCFOutputDto>().ReverseMap();
            CreateMap<RCFInputDto, CustomerRC>();//.IgnoreAllPropertiesWithAnInaccessibleSetteer();

            CreateMap<Externalform, ExternalformOutputDto>().ReverseMap();
            CreateMap<ExternalformInputDto, Externalform>();

            CreateMap<Internalform, InternalformOutputDto>().ReverseMap();
            CreateMap<InternalformInPutDto, Internalform>();

            CreateMap<PDFDataModel, CustomerLC>().ReverseMap();
            CreateMap<PDFDataModel, CustomerLN>().ReverseMap();
            CreateMap<PDFDataModel, CustomerRN>().ReverseMap();
            CreateMap<PDFDataModel, CustomerRC>().ReverseMap();
            CreateMap<PDFDataModel, Building>().ReverseMap();
            CreateMap<PDFDataModel, Building1>().ReverseMap();
            CreateMap<PDFDataModel, RemittanceL>().ReverseMap();
            CreateMap<PDFDataModel, B030101>().ReverseMap();
            CreateMap<PDFDataModel, TBNoB1>().ReverseMap();
            CreateMap<PDFDataModel, TBNoB2>().ReverseMap();
            CreateMap<PDFDataModel, TBNoB3>().ReverseMap();
            CreateMap<PDFDataModel, TBNoB4>().ReverseMap();
            CreateMap<PDFDataModel, TBNoB5>().ReverseMap();
            CreateMap<PDFDataModel, TBNoB6>().ReverseMap();
            CreateMap<PDFDataModel, TBNoB1_2>().ReverseMap();
            CreateMap<PDFDataModel, TBNoB2_2>().ReverseMap();
            CreateMap<PDFDataModel, TBNoB3_2>().ReverseMap();
            CreateMap<PDFDataModel, TBNoB4_2>().ReverseMap();
            CreateMap<PDFDataModel, TBNoB5_2>().ReverseMap();
            CreateMap<ContractPDFDataModel, TBNoC1>().ReverseMap();
            CreateMap<ContractPDFDataModel, TBNoC1_2>().ReverseMap();
            CreateMap<ContractPDFDataModel, TBNoC1_3>().ReverseMap();
            CreateMap<ContractPDFDataModel, TBNoC2>().ReverseMap();
            CreateMap<ContractPDFDataModel, TBNoC2_2>().ReverseMap();
            CreateMap<ContractPDFDataModel, TBNoC2_3>().ReverseMap();
            CreateMap<ContractPDFDataModel, TBNoC3>().ReverseMap();
            CreateMap<ContractPDFDataModel, TBNoC3_2>().ReverseMap();
            CreateMap<ContractPDFDataModel, TBNoC3_3>().ReverseMap();
            CreateMap<ContractPDFDataModel, Contract>().ReverseMap();
            CreateMap<ContractPDFDataModel, SecondLandlord>().ReverseMap();
            CreateMap<ContractPDFDataModel, ContractLandlord>().ReverseMap();
            CreateMap<ContractPDFDataModel, ContractRenter>().ReverseMap();
            CreateMap<ContractPDFDataModel, ContractBuilding>().ReverseMap();
            CreateMap<ContractPDFDataModel, ContractBuildingOutputDto>().ReverseMap();
            CreateMap<ContractPDFDataModel, ContractRelevant>().ReverseMap();
            CreateMap<ContractPDFDataModel, ContractRelevantOutputDto>().ReverseMap();
            CreateMap<ContractBuilding, ContractBuildingOutputDto>().ReverseMap();
            CreateMap<CustomerRN, ContractRenter>().ReverseMap();
            CreateMap<Building, ContractBuilding> ().ReverseMap();
            CreateMap<Building, ContractBuildingOutputDto>().ReverseMap();
            CreateMap<Building1, ContractBuilding>().ReverseMap();
            CreateMap<Building1, ContractBuildingOutputDto>().ReverseMap();


            //Web表單
            CreateMap<WebFormDataModel, A000002OutputDto>().ReverseMap();
            CreateMap<WebFormDataModel, A000002InputDto>().ReverseMap();
            CreateMap<WebFormDataModel, A000001OutputDto>().ReverseMap();
            CreateMap<WebFormDataModel, A000001InputDto>().ReverseMap();
            CreateMap<WebFormDataModel, A000003OutputDto>().ReverseMap();
            CreateMap<WebFormDataModel, A000003InputDto>().ReverseMap();
            CreateMap<WebFormDataModel, A000201OutputDto>().ReverseMap();
            CreateMap<WebFormDataModel, A000201InputDto>().ReverseMap();
            CreateMap<WebFormDataModel, A000203OutputDto>().ReverseMap();
            CreateMap<WebFormDataModel, A000203InputDto>().ReverseMap();
            CreateMap<WebFormDataModel, A000202OutputDto>().ReverseMap();
            CreateMap<WebFormDataModel, A000202InputDto>().ReverseMap();
            CreateMap<WebFormDataModel, A000004OutputDto>().ReverseMap();
            CreateMap<WebFormDataModel, A000004InputDto>().ReverseMap();

            CreateMap<A000002InputDto, TBNoB6>().ReverseMap();
            CreateMap<A000001InputDto, TBNoB6>().ReverseMap();
            CreateMap<A000003InputDto, TBNoB6>().ReverseMap();
            CreateMap<A000004InputDto, TBNoB6>().ReverseMap();
            CreateMap<A000201InputDto, TBNoB6>().ReverseMap();
            CreateMap<A000202InputDto, TBNoB6>().ReverseMap();
            CreateMap<A000203InputDto, TBNoB6>().ReverseMap();

            CreateMap<A000002InputDto, CustomerRN>().ReverseMap();

            CreateMap<A000001InputDto, CustomerLN>().ReverseMap();
            CreateMap<A000001InputDto, RemittanceL>().ReverseMap();
            CreateMap<A000001InputDto, Building>().ReverseMap();
            CreateMap<A000001InputDto, Building1>().ReverseMap();

            CreateMap<A000003InputDto, CustomerLC>().ReverseMap();
            CreateMap<A000003InputDto, RemittanceL>().ReverseMap();

            CreateMap<A000201InputDto, CustomerLN>().ReverseMap();
            CreateMap<A000201InputDto, CustomerLC>().ReverseMap();
            CreateMap<A000201InputDto, Building>().ReverseMap();
            CreateMap<A000201InputDto, Building1>().ReverseMap();

            CreateMap<A000202InputDto, CustomerLN>().ReverseMap();
            CreateMap<A000202InputDto, CustomerLC>().ReverseMap();
            CreateMap<A000202InputDto, Building>().ReverseMap();
            CreateMap<A000202InputDto, Building1>().ReverseMap();

            CreateMap<A000203InputDto, CustomerLN>().ReverseMap();
            CreateMap<A000203InputDto, CustomerLC>().ReverseMap();
            CreateMap<A000203InputDto, Building>().ReverseMap();
            CreateMap<A000203InputDto, Building1>().ReverseMap();

            CreateMap<A000004InputDto, CustomerRC>().ReverseMap();
            CreateMap<A000004InputDto, RemittanceR>().ReverseMap();

            // 表單分類
            CreateMap<CategoryForm, CategoryFormOutputDto>().ReverseMap();
            CreateMap<CategoryFormInputDto, CategoryForm>();
            CreateMap<CategoryContract, CategoryContractOutputDto>().ReverseMap();
            CreateMap<CategoryContractInputDto, CategoryContract>();

            // 領收據
            CreateMap<Receipt, ReceiptOutputDto>().ReverseMap();
            CreateMap<ReceiptInputDto, Receipt>();

            // 合約
            CreateMap<Contract, ContractOutputDto>().ReverseMap();
            CreateMap<ContractInputDto, Contract>();
            CreateMap<ManagementOutputDto, Contract>().ReverseMap();
            CreateMap<SecondLandlordOutputDto, Contract>().ReverseMap();
            CreateMap<SecondLandlord, SecondLandlordOutputDto>().ReverseMap();
            CreateMap<SecondLandlordInputDto, SecondLandlord>();
            CreateMap<Management, ManagementOutputDto>().ReverseMap();
            CreateMap<ManagementInputDto, Management>();
            CreateMap<SLMA, SLMAOutputDto>().ReverseMap();
            CreateMap<SLMAInputDto, SLMA>();
            CreateMap<ContractAttachment, ContractAttachmentOutputDto>().ReverseMap();
            CreateMap<ContractAttachmentInputDto, ContractAttachment>();
            CreateMap<ContractRelevant, ContractRelevantOutputDto>().ReverseMap();
            CreateMap<ContractRelevantInputDto, ContractRelevant>();
            CreateMap<HistoryFormContract, HistoryFormContractOutputDto>().ReverseMap();
            CreateMap<HistoryFormContractInputDto, HistoryFormContract>();
            CreateMap<ContractFlowLog, ContractFlowLogOutputDto>().ReverseMap();
            CreateMap<ContractFlowLogInputDto, ContractFlowLog>();
            CreateMap<TBNoC1, TBNoCOutputDto>().ReverseMap();
            CreateMap<TBNoC2, TBNoCOutputDto>().ReverseMap();
            CreateMap<TBNoC3, TBNoCOutputDto>().ReverseMap();
            CreateMap<TBNoCInputDto, TBNoC1>();
            CreateMap<TBNoCInputDto, TBNoC2>();
            CreateMap<TBNoCInputDto, TBNoC3>();
            CreateMap<TBNoC1_2, TBNoC_2OutputDto>().ReverseMap();
            CreateMap<TBNoC2_2, TBNoC_2OutputDto>().ReverseMap();
            CreateMap<TBNoC3_2, TBNoC_2OutputDto>().ReverseMap();
            CreateMap<TBNoC_2InputDto, TBNoC1_2>();
            CreateMap<TBNoC_2InputDto, TBNoC2_2>();
            CreateMap<TBNoC_2InputDto, TBNoC3_2>();
            CreateMap<TBNoC1_3, TBNoC_3OutputDto>().ReverseMap();
            CreateMap<TBNoC2_3, TBNoC_3OutputDto>().ReverseMap();
            CreateMap<TBNoC3_3, TBNoC_3OutputDto>().ReverseMap();
            CreateMap<TBNoC_3InputDto, TBNoC1_3>();
            CreateMap<TBNoC_3InputDto, TBNoC2_3>();
            CreateMap<TBNoC_3InputDto, TBNoC3_3>();
            //CreateMap<TBNoC1InputDto, TBNoC1>();
            //CreateMap<TBNoC1_2InputDto, TBNoC1_2>();
            //CreateMap<TBNoC1_3InputDto, TBNoC1_3>();
            //CreateMap<TBNoC2InputDto, TBNoC2>();
            //CreateMap<TBNoC2_2InputDto, TBNoC2_2>();
            //CreateMap<TBNoC2_3InputDto, TBNoC2_3>();
            //CreateMap<TBNoC3InputDto, TBNoC3>();
            //CreateMap<TBNoC3_2InputDto, TBNoC3_2>();
            //CreateMap<TBNoC3_3InputDto, TBNoC3_3>();
            //CreateMap<TBNoC1, TBNoC1OutputDto>().ReverseMap();
            //CreateMap<TBNoC1_2, TBNoC1_2OutputDto>().ReverseMap();
            //CreateMap<TBNoC1_3, TBNoC1_3OutputDto>().ReverseMap();
            //CreateMap<TBNoC2, TBNoC2OutputDto>().ReverseMap();
            //CreateMap<TBNoC2_2, TBNoC2_2OutputDto>().ReverseMap();
            //CreateMap<TBNoC2_3, TBNoC2_3OutputDto>().ReverseMap();
            //CreateMap<TBNoC3, TBNoC3OutputDto>().ReverseMap();
            //CreateMap<TBNoC3_2, TBNoC3_2OutputDto>().ReverseMap();
            //CreateMap<TBNoC3_3, TBNoC3_3OutputDto>().ReverseMap();

            // 合約分類
            CreateMap<Contractform, ContractformOutputDto>().ReverseMap();
            CreateMap<ContractformInputDto, Contractform>();

            // 多物件管理
            CreateMap<MO, MOOutputDto>().ReverseMap();
            CreateMap<MOInputDto, MO>();
            CreateMap<MOBuilding, MOBuildingOutputDto>().ReverseMap();
            CreateMap<MOBuildingInputDto, MOBuilding>();
            CreateMap<MOBuilding, Building>();

            // 潛在客管理
            CreateMap<PotentialCustomers, PotentialCustomersOutputDto>().ReverseMap(); ;
            CreateMap<PotentialCustomersInputDto, PotentialCustomers>();
            CreateMap<VisitingRecord, VisitingRecordOutputDto>().ReverseMap(); ;
            CreateMap<VisitingRecordInputDto, VisitingRecord>();
            CreateMap<PotentialCustomersInputDto, Building>();

            // 待辦事項
            CreateMap<ToDoList, ToDoListOutputDto>().ReverseMap();
            CreateMap<ToDoListInputDto, ToDoList>();
        }
    }
}
