using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace apoemaMatch.Data
{
    public enum EnumLinhaDeAtuacaoTI
    {
        [Display(Name = "Desenvolvimento ERP/SIG/BPM")]
        Desenvolvimento_ERP_SIG_BPM = 1,
        [Display(Name = "Suporte técnico/Infraestrutura TI")]
        Suporte_Tecnico_Infra_TI,
        [Display(Name = "Assessoria/Consultoria TI")]
        Assessoria_Consultoria_TI,
        [Display(Name = "Outsourcing BPO/KPO")]
        Outsourcing_BPO_KPO,
        [Display(Name = "Desenvolvimento FullStack")]
        Desenvolvimento_Fullstack,
        [Display(Name = "Desenvolvimento Mobile")]
        Desenvolvimento_Mobile,
        [Display(Name = "Desenvolvimento CRM/BI")]
        Desenvolvimento_CRM_BI,
        [Display(Name = "Desenvolvimento Cloud/BigData")]
        Desenvolvimento_Cloud_BigData,
        [Display(Name = "Desenvolvimento Data Science/Machine Learning/Deep Learning")]
        Desenvolvimento_DataScience_MachineLearning_DeepLearning,
        [Display(Name = "Certificadora Homologadora")]
        Certificadora_Homologadora,
        [Display(Name = "Capacitação/Qualificação")]
        Capacitacao_Qualificacao,
        [Display(Name = "Mentoring Coaching")]
        Mentoring_Coaching,
        [Display(Name = "Outros")]
        Outros
    }
}
