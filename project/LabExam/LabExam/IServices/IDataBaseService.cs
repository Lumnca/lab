using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabExam.DataSource;
using LabExam.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LabExam.IServices
{
    public interface IDataBaseService
    {
        float GetPaperScore(int paperId, LabContext context);

        void FinishPaper(ExaminationPaper paper, LabContext context);

        Boolean DeletePaper(ExaminationPaper ePaper, LabContext context);
    }
}
