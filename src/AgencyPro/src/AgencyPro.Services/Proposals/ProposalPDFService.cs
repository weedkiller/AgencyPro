// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Contracts.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Proposals.Extensions;
using AgencyPro.Core.Proposals.Models;
using AgencyPro.Core.Proposals.Services;
using AgencyPro.Core.Proposals.ViewModels;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Font = iTextSharp.text.Font;
using Image = iTextSharp.text.Image;
using Rectangle = iTextSharp.text.Rectangle;

namespace AgencyPro.Services.Proposals
{
    public class ProposalPDFService : Service<FixedPriceProposal>, IProposalPDFService
    {
        private readonly ILogger<ProposalPDFService> _logger;

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(ProposalPDFService)}.{callerName}] - {message}";
        }

        public ProposalPDFService(IServiceProvider serviceProvider, ILogger<ProposalPDFService> logger) : base(serviceProvider)
        {
            _logger = logger;
        }

        #region Generate Proposal - AO

        public async Task<FileStreamResult> GenerateProposal(IProviderAgencyOwner ao, Guid proposalId)
        {
            _logger.LogInformation(GetLogMessage("Proposal ID: {0}"), proposalId);

            var proposal = await Repository.Queryable()
                .ForAgencyOwner(ao).FindById(proposalId)
                .ProjectTo<AgencyOwnerFixedPriceProposalDetailsOutput>(ProjectionMapping)
                .FirstOrDefaultAsync();

            return proposal != null ? CreatePdf(proposal) : null;
        }

        public async Task<FileStreamResult> GenerateProposal(Guid proposalId)
        {
            _logger.LogInformation(GetLogMessage("Proposal ID: {0}"), proposalId);

            var proposal = await Repository.Queryable()
                .FindById(proposalId)
                .ProjectTo<AgencyOwnerFixedPriceProposalDetailsOutput>(ProjectionMapping)
                .FirstOrDefaultAsync();

            return proposal != null ? CreatePdf(proposal) : null;
        }

        #endregion

        #region GenerateProposal - AM

        public async Task<FileStreamResult> GenerateProposal(IOrganizationAccountManager am, Guid proposalId)
        {
            _logger.LogInformation(GetLogMessage("Proposal ID: {0}"), proposalId);

            var proposal = await Repository.Queryable()
                .ForOrganizationAccountManager(am).FindById(proposalId)
                .ProjectTo<AgencyOwnerFixedPriceProposalDetailsOutput>(ProjectionMapping)
                .FirstOrDefaultAsync();

            return proposal != null ? CreatePdf(proposal) : null;
        }

        #endregion

        #region GenerateProposal - CU

        public async Task<FileStreamResult> GenerateProposal(IOrganizationCustomer cu, Guid proposalId)
        {
            _logger.LogInformation(GetLogMessage("Proposal ID: {0}"), proposalId);

            var proposal = await Repository.Queryable()
                .ForOrganizationCustomer(cu).FindById(proposalId)
                .ProjectTo<AgencyOwnerFixedPriceProposalDetailsOutput>(ProjectionMapping)
                .FirstOrDefaultAsync();

            return proposal != null ? CreatePdf(proposal) : null;
        }

        #endregion

        #region Create Pdf

        private FileStreamResult CreatePdf(AgencyOwnerFixedPriceProposalDetailsOutput proposal)
        {
            MemoryStream stream = new MemoryStream();

            Document pdfDoc = new Document(PageSize.A4, 30f, 30f, 20f, 20f);

            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);

            pdfDoc.Open();

            AddContentToPdf(proposal, pdfDoc);

            pdfDoc.Close();

            writer.Close();

            stream.Position = 0;

            return new FileStreamResult(stream, "application/pdf")
            {
                FileDownloadName = proposal.Id + ".pdf"
            };
        }

        private void AddContentToPdf(AgencyOwnerFixedPriceProposalDetailsOutput proposal, Document pdfDoc)
        {
            AddProposalDetailToPdf(proposal, pdfDoc);
            AddAgreementToPdf(proposal, pdfDoc);
            AddContractToPdf(proposal, pdfDoc);
            AddStoryToPdf(proposal, pdfDoc);
        }

        #endregion

        #region Fonts

        private static BaseFont GetBaseFont()
        {
            return BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);
        }

        private static Font GetCommonFont(int fontSize = 10)
        {
            return new Font(GetBaseFont(), fontSize, Font.NORMAL, new BaseColor(50, 56, 98));
        }

        private static Font GetGreyFont(int fontSize = 10)
        {
            return new Font(GetBaseFont(), fontSize, Font.NORMAL, BaseColor.Gray);
        }

        #endregion

        #region Add Proposal Detail To Pdf

        private static void AddProposalDetailToPdf(AgencyOwnerFixedPriceProposalDetailsOutput proposal, Document pdfDoc)
        {
            PdfPTable proposalDetailTable = new PdfPTable(2) { WidthPercentage = 100 };

            proposalDetailTable.SetWidths(new float[] { 1, 3 });

            AddLeftRightContentToPdf(proposal, proposalDetailTable);

            pdfDoc.Add(proposalDetailTable);
        }

        private static void AddLeftRightContentToPdf(AgencyOwnerFixedPriceProposalDetailsOutput proposal, PdfPTable layoutTable)
        {
            AddLogoToPdf(proposal, layoutTable);

            PdfPCell leftDetails = new PdfPCell { Border = Rectangle.NO_BORDER };
            leftDetails.AddElement(CreateLeftTable(proposal));
            layoutTable.AddCell(leftDetails);

            PdfPCell rightDetails = new PdfPCell { Border = Rectangle.NO_BORDER };
            rightDetails.AddElement(CreateRightTable(proposal));
            layoutTable.AddCell(rightDetails);
        }

        private static void AddLogoToPdf(AgencyOwnerFixedPriceProposalDetailsOutput proposal, PdfPTable layoutTable)
        {
            PdfPCell orgImageCell = new PdfPCell
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_TOP,
                MinimumHeight = 80
            };

            if (!string.IsNullOrEmpty(proposal.AccountManagerOrganizationImageUrl?.Trim()))
            {
                Image orgImage = Image.GetInstance(proposal.AccountManagerOrganizationImageUrl);
                orgImage.ScaleToFit(70, 70);
                orgImage.Alignment = Element.ALIGN_LEFT | Element.ALIGN_TOP;
                orgImageCell.AddElement(orgImage);
            }

            layoutTable.AddCell(orgImageCell);

            var fontHeader = GetCommonFont(22);

            AddCellToTable(layoutTable, fontHeader, "Proposal", Element.ALIGN_RIGHT, 1, 2);
            AddCellToTable(layoutTable, fontHeader, "", Element.ALIGN_RIGHT, 1, 3);
        }

        public static PdfPTable CreateLeftTable(AgencyOwnerFixedPriceProposalDetailsOutput proposal)
        {
            var pdfTable = new PdfPTable(1)
            {
                WidthPercentage = 100
            };

            CreateEntityInPdf(pdfTable, "Account Manager",
                proposal.AccountManagerName, proposal.AccountManagerOrganizationName);

            CreateEntityInPdf(pdfTable, "Project Manager",
                proposal.ProjectManagerName, proposal.ProjectManagerOrganizationName);

            CreateEntityInPdf(pdfTable, "Customer",
                proposal.CustomerName, proposal.CustomerOrganizationName);

            return pdfTable;
        }

        public static PdfPTable CreateRightTable(AgencyOwnerFixedPriceProposalDetailsOutput proposal)
        {
            var pdfTable = new PdfPTable(2)
            {
                WidthPercentage = 49,
                HorizontalAlignment = Element.ALIGN_RIGHT
            };

            pdfTable.SetWidths(new[] { 3f, 1f });

            Font fontHeader = GetGreyFont();
            Font fontNormal = GetCommonFont();

            CreateDetailInPdf(pdfTable, "", proposal.ProjectName, fontHeader, fontNormal, 1, 2);

            CreateDetailInPdf(pdfTable, "STATUS", proposal.Status.ToString(), fontHeader, fontNormal);

            CreateDetailInPdf(pdfTable, "PROPOSAL TYPE", proposal.ProposalType.ToString(), fontHeader, fontNormal);

            CreateDetailInPdf(pdfTable, "PROPOSAL PRICE", "$" + proposal.TotalPriceQuoted.ToString("N2"), fontHeader,
                fontNormal);

            CreateDetailInPdf(pdfTable, "TOTAL HOURS", proposal.TotalHours.ToString(), fontHeader, fontNormal);

            CreateDetailInPdf(pdfTable, "CUSTOMER RATE BASIS", "$" + proposal.CustomerRateBasis.ToString(), fontHeader, fontNormal);

            CreateDetailInPdf(pdfTable, "STORY POINT BASIS", proposal.StoryPointBasis.ToString(), fontHeader, fontNormal);

            CreateDetailInPdf(pdfTable, "WEEKLY MAX HOUR BASIS", proposal.WeeklyMaxHourBasis.ToString(), fontHeader, fontNormal);

            return pdfTable;
        }

        #endregion

        #region Add Agreement To Pdf

        private static void AddAgreementToPdf(AgencyOwnerFixedPriceProposalDetailsOutput proposal, Document pdfDoc)
        {
            Paragraph agreementHeader = new Paragraph(new Phrase("Agreement Terms", GetCommonFont(13)))
            {
                SpacingAfter = 10f,
                Alignment = Element.ALIGN_LEFT
            };

            Paragraph agreementContent = new Paragraph(new Phrase(proposal.AgreementText?.Trim() ?? " ",
                GetGreyFont()))
            {
                SpacingAfter = 15f,
                Alignment = Element.ALIGN_JUSTIFIED
            };

            pdfDoc.Add(agreementHeader);
            pdfDoc.Add(agreementContent);
        }

        #endregion

        #region Add Contract To Pdf

        private void AddContractToPdf(AgencyOwnerFixedPriceProposalDetailsOutput proposal, Document pdfDoc)
        {
            Paragraph contractHeader = new Paragraph(new Phrase("Contracts", GetCommonFont(13)))
            {
                SpacingAfter = 15f
            };

            pdfDoc.Add(contractHeader);

            var pdfTable = new PdfPTable(3)
            {
                WidthPercentage = 100,
                HorizontalAlignment = Element.ALIGN_LEFT,
                HeaderRows = 1
            };

            if (proposal.ProposedContracts.Any())
            {
                pdfTable.SetWidths(new float[] {3, 1, 1});

                CreateContractorTable(proposal, pdfTable);
            }

            pdfDoc.Add(pdfTable);
        }

        private void CreateContractorTable(AgencyOwnerFixedPriceProposalDetailsOutput proposal, PdfPTable pdfTable)
        {
            var fontTableHeader = GetCommonFont();

            PdfPRow headerRow = new PdfPRow(new[]
            {
                AddHeaderToDetailTable(fontTableHeader, "CONTRACTOR"),
                AddHeaderToDetailTable(fontTableHeader, "CUSTOMER RATE"),
                AddHeaderToDetailTable(fontTableHeader, "CU MAX WEEKLY")
            });

            pdfTable.Rows.Add(headerRow);

            int i = 0;
            var backgroundColor = new BaseColor(246, 249, 252);
            foreach (var proposedContract in proposal.ProposedContracts)
            {
                CreateContractorRow(proposedContract, pdfTable, fontTableHeader,
                    i % 2 != 0 ? backgroundColor : BaseColor.White);
                i++;
            }
        }

        private static void CreateContractorRow(ProposedContractOutput proposedContract, PdfPTable pdfTable,
            Font fontTableHeader, BaseColor backgroundColor)
        {
            PdfPCell contractCell = new PdfPCell
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Border = Rectangle.NO_BORDER,
                BackgroundColor = backgroundColor
            };

            var pdfTableDetail = new PdfPTable(1)
            {
                WidthPercentage = 100
            };

            CreateEntityInPdf(pdfTableDetail, "", proposedContract.ContractorName,
                proposedContract.ContractorOrganizationName, false);

            contractCell.AddElement(pdfTableDetail);

            PdfPRow detailRow = new PdfPRow(new[]
            {
                contractCell,
                AddCellToDetailTable(fontTableHeader, "$" + proposedContract.CustomerRate.ToString("N2"),
                    Element.ALIGN_RIGHT, backgroundColor, true),
                AddCellToDetailTable(fontTableHeader, "$" + proposedContract.MaxCustomerWeekly.ToString("N2"),
                    Element.ALIGN_RIGHT, backgroundColor, true)
            });

            pdfTable.Rows.Add(detailRow);
        }

        #endregion

        #region Add Story To Pdf

        private void AddStoryToPdf(AgencyOwnerFixedPriceProposalDetailsOutput proposal, Document pdfDoc)
        {
            Paragraph storyHeader = new Paragraph(new Phrase("Stories", GetCommonFont(13)))
            {
                SpacingBefore = 10f,
                SpacingAfter = 15f
            };

            pdfDoc.Add(storyHeader);

            var storyTable = new PdfPTable(2)
            {
                WidthPercentage = 100,
                HorizontalAlignment = Element.ALIGN_LEFT,
                HeaderRows = 1
            };

            if (proposal.ProposedStories.Any())
            {
                storyTable.SetWidths(new float[] { 4, 1 });
                CreateStoryTable(proposal, storyTable);
            }

            pdfDoc.Add(storyTable);
        }

        private void CreateStoryTable(AgencyOwnerFixedPriceProposalDetailsOutput proposal, PdfPTable pdfTable)
        {
            var font = GetCommonFont();

            PdfPRow headerRow = new PdfPRow(new[]
            {
                AddHeaderToDetailTable(font, "TITLE", Element.ALIGN_LEFT),
                AddHeaderToDetailTable(font, "STORY POINTS", Element.ALIGN_RIGHT)
            });

            pdfTable.Rows.Add(headerRow);

            int i = 0;
            var backgroundColor = new BaseColor(246, 249, 252);
            foreach (var proposedStory in proposal.ProposedStories)
            {
                PdfPRow dataRow = new PdfPRow(new[]
                {
                    AddCellToDetailTable(font, proposedStory.Title, Element.ALIGN_LEFT,
                        i % 2 != 0 ? backgroundColor : BaseColor.White),
                    AddCellToDetailTable(font, (proposedStory.StoryPoints ?? 0).ToString(),
                        Element.ALIGN_RIGHT, i % 2 != 0 ? backgroundColor : BaseColor.White)
                });

                pdfTable.Rows.Add(dataRow);
                i++;
            }
        }

        #endregion

        #region Common Methods

        private static void AddCellToTable(PdfPTable table, Font font, string text, int horAlign = Element.ALIGN_LEFT,
            int rowSpan = 1, int colSpan = 1)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, font))
            {
                Rowspan = rowSpan,
                HorizontalAlignment = horAlign,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Colspan = colSpan,
                Border = Rectangle.NO_BORDER
            };
            table.AddCell(cell);
        }

        private static PdfPCell AddHeaderToDetailTable(Font font, string text, int horizontalAlign = Element.ALIGN_CENTER)
        {
            return new PdfPCell(new Phrase(text, font))
            {
                HorizontalAlignment = horizontalAlign,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                MinimumHeight = 30,
                Border = Rectangle.NO_BORDER,
                BackgroundColor = new BaseColor(230, 235, 241),
                PaddingRight = 10f,
                PaddingLeft = 10f
            };
        }

        private static PdfPCell AddCellToDetailTable(Font font, string text, int horizontalAlign,
            BaseColor baseColor, bool isContractorTable = false)
        {
            var pdfPCell = new PdfPCell(new Phrase(text, font))
            {
                HorizontalAlignment = horizontalAlign,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Border = Rectangle.NO_BORDER,
                BackgroundColor = baseColor,
                MinimumHeight = isContractorTable ? 35 : 20,
                PaddingRight = 10f,
                PaddingLeft = 10f
            };

            return pdfPCell;
        }

        private static void CreateEntityInPdf(PdfPTable pdfTable, string entityHeaderName,
            string entityName, string organizationName, bool isAddPadding = true)
        {
            if (!string.IsNullOrEmpty(entityHeaderName?.Trim()))
                AddCellToTable(pdfTable, GetGreyFont(12), entityHeaderName);

            AddCellToTable(pdfTable, GetCommonFont(11), entityName);

            AddCellToTable(pdfTable, GetGreyFont(), organizationName);

            if (isAddPadding)
                AddCellToTable(pdfTable, GetCommonFont(11), " ");
        }

        private static void CreateDetailInPdf(PdfPTable pdfTable, string entityHeaderName, string entityValue,
            Font fontHeader, Font fontNormal, int rowSpan = 1, int colSpan = 1)
        {
            if (!string.IsNullOrEmpty(entityHeaderName))
                AddCellToTable(pdfTable, fontHeader, entityHeaderName);

            AddCellToTable(pdfTable, fontNormal, entityValue, Element.ALIGN_RIGHT, rowSpan, colSpan);
        }

        #endregion

        private string GetFilePath(Guid proposalId)
        {
            return GetImagePath(proposalId.ToString());
        }

        public static string GetBaseDir()
        {
            var currentAssembly = typeof(ProposalPDFService).GetTypeInfo().Assembly;
            var root = Path.GetDirectoryName(currentAssembly.Location);
            var idx = root.IndexOf($"{Path.DirectorySeparatorChar}bin", StringComparison.OrdinalIgnoreCase);
            return root.Substring(0, idx);
        }

        public static string GetImagePath(string fileName)
        {

            return Path.Combine(GetBaseDir(), "img", fileName);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetOutputFileName([CallerMemberName] string methodName = null)
        {
            return Path.Combine(GetOutputFolder(), $"{methodName}.pdf");
        }

        public static string GetOutputFolder()
        {
            var dir = Path.Combine(GetBaseDir(), "bin", "out");
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            return dir;
        }

    }
}
