﻿using System;
using System.Collections.Generic;
using CIM.Model;
using FTN.Common;
using FTN.ESI.SIMES.CIM.CIMAdapter.Manager;

namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	/// <summary>
	/// LabsProfileImporter
	/// </summary>
	public class LabsProfileImporter
	{
		/// <summary> Singleton </summary>
		private static LabsProfileImporter ptImporter = null;
		private static object singletoneLock = new object();

		private ConcreteModel concreteModel;
		private Delta delta;
		private ImportHelper importHelper;
		private TransformAndLoadReport report;


		#region Properties
		public static LabsProfileImporter Instance
		{
			get
			{
				if (ptImporter == null)
				{
					lock (singletoneLock)
					{
						if (ptImporter == null)
						{
							ptImporter = new LabsProfileImporter();
							ptImporter.Reset();
						}
					}
				}
				return ptImporter;
			}
		}

		public Delta NMSDelta
		{
			get 
			{
				return delta;
			}
		}
		#endregion Properties


		public void Reset()
		{
			concreteModel = null;
			delta = new Delta();
			importHelper = new ImportHelper();
			report = null;
		}

		public TransformAndLoadReport CreateNMSDelta(ConcreteModel cimConcreteModel)
		{
			LogManager.Log("Importing LabsProfile Elements...", LogLevel.Info);
			report = new TransformAndLoadReport();
			concreteModel = cimConcreteModel;
			delta.ClearDeltaOperations();

			if ((concreteModel != null) && (concreteModel.ModelMap != null))
			{
				try
				{
					// convert into DMS elements
					ConvertModelAndPopulateDelta();
				}
				catch (Exception ex)
				{
					string message = string.Format("{0} - ERROR in data import - {1}", DateTime.Now, ex.Message);
					LogManager.Log(message);
					report.Report.AppendLine(ex.Message);
					report.Success = false;
				}
			}
			LogManager.Log("Importing LabsProfile Elements - END.", LogLevel.Info);
			return report;
		}

		/// <summary>
		/// Method performs conversion of network elements from CIM based concrete model into DMS model.
		/// </summary>
		private void ConvertModelAndPopulateDelta()
		{
			LogManager.Log("Loading elements and creating delta...", LogLevel.Info);

            //// import all concrete model types (DMSType enum)

            ImportConnectivityNodes();
            ImportTerminals();
            ImportSeriesCompensators();
            ImportDCLineSegments();
            ImportACLineSegments();
            ImportPerLengthSequenceImpedances();

			LogManager.Log("Loading elements and creating delta completed.", LogLevel.Info);
		}

		#region Import
		private void ImportConnectivityNodes()
		{
			SortedDictionary<string, object> cimConnectivityNodes = concreteModel.GetAllObjectsOfType("FTN.ConnectivityNode");
			if (cimConnectivityNodes != null)
			{
				foreach (KeyValuePair<string, object> cimConnectivityNodesPair in cimConnectivityNodes)
				{
                    
                    FTN.ConnectivityNode cimConnectivityNode = cimConnectivityNodesPair.Value as FTN.ConnectivityNode;

					ResourceDescription rd = CreateConnectivityNodeResourceDescription(cimConnectivityNode);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("ConnectivityNode ID = ").Append(cimConnectivityNode.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("ConnectivityNode ID = ").Append(cimConnectivityNode.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}
		private ResourceDescription CreateConnectivityNodeResourceDescription(FTN.ConnectivityNode cimConnectivityNode)
		{
			ResourceDescription rd = null;
			if (cimConnectivityNode != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.CONNODE, importHelper.CheckOutIndexForDMSType(DMSType.CONNODE));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimConnectivityNode.ID, gid);

				////populate ResourceDescription
				LabsProfileConverter.PopulateConnectivityNodeProperties(cimConnectivityNode, rd);
			}
			return rd;
		}

        private void ImportTerminals()
        {
            SortedDictionary<string, object> cimTerminals = concreteModel.GetAllObjectsOfType("FTN.Terminal");
            if (cimTerminals != null)
            {
                foreach (KeyValuePair<string, object> cimTerminalPair in cimTerminals)
                {
                    FTN.Terminal cimTerminal = cimTerminalPair.Value as FTN.Terminal;

                    ResourceDescription rd = CreateTerminalResourceDescription(cimTerminal);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("Terminal ID = ").Append(cimTerminal.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("Terminal ID = ").Append(cimTerminal.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }
        private ResourceDescription CreateTerminalResourceDescription(FTN.Terminal cimTerminal)
        {
            ResourceDescription rd = null;
            if (cimTerminal != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.TERMINAL, importHelper.CheckOutIndexForDMSType(DMSType.TERMINAL));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimTerminal.ID, gid);

                ////populate ResourceDescription
                LabsProfileConverter.PopulateTerminalProperties(cimTerminal, rd, importHelper, report);
            }
            return rd;
        }

        private void ImportSeriesCompensators()
        {
            SortedDictionary<string, object> cimSeriesCompensators = concreteModel.GetAllObjectsOfType("FTN.SeriesCompensator");
            if (cimSeriesCompensators != null)
            {
                foreach (KeyValuePair<string, object> cimSeriesCompensatorPair in cimSeriesCompensators)
                {
                    FTN.SeriesCompensator cimSeriesCompensator = cimSeriesCompensatorPair.Value as FTN.SeriesCompensator;

                    ResourceDescription rd = CreateSeriesCompensatorResourceDescription(cimSeriesCompensator);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("SeriesCompensator ID = ").Append(cimSeriesCompensator.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("SeriesCompensator ID = ").Append(cimSeriesCompensator.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }
        private ResourceDescription CreateSeriesCompensatorResourceDescription(FTN.SeriesCompensator cimSeriesCompensator)
        {
            ResourceDescription rd = null;
            if (cimSeriesCompensator != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.SERCOMPENSATOR, importHelper.CheckOutIndexForDMSType(DMSType.SERCOMPENSATOR));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimSeriesCompensator.ID, gid);

                ////populate ResourceDescription
                LabsProfileConverter.PopulateSeriesCompensatorProperties(cimSeriesCompensator, rd, importHelper, report);
            }
            return rd;
        }

        private void ImportACLineSegments()
        {
            SortedDictionary<string, object> cimACLineSegments = concreteModel.GetAllObjectsOfType("FTN.ACLineSegment");
            if (cimACLineSegments != null)
            {
                foreach (KeyValuePair<string, object> cimACLineSegmentPair in cimACLineSegments)
                {
                    FTN.ACLineSegment cimACLineSegment = cimACLineSegmentPair.Value as FTN.ACLineSegment;

                    ResourceDescription rd = CreateACLineSegmentResourceDescription(cimACLineSegment);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("ACLineSegment ID = ").Append(cimACLineSegment.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("ACLineSegment ID = ").Append(cimACLineSegment.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }
        private ResourceDescription CreateACLineSegmentResourceDescription(FTN.ACLineSegment cimACLineSegment)
        {
            ResourceDescription rd = null;
            if (cimACLineSegment != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.ACLINESEG, importHelper.CheckOutIndexForDMSType(DMSType.ACLINESEG));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimACLineSegment.ID, gid);

                ////populate ResourceDescription
                LabsProfileConverter.PopulateACLineSegmentProperties(cimACLineSegment, rd, importHelper, report);
            }
            return rd;
        }

        private void ImportDCLineSegments()
        {
            SortedDictionary<string, object> cimDCLineSegments = concreteModel.GetAllObjectsOfType("FTN.DCLineSegment");
            if (cimDCLineSegments != null)
            {
                foreach (KeyValuePair<string, object> cimDCLineSegmentPair in cimDCLineSegments)
                {
                    FTN.DCLineSegment cimDCLineSegment = cimDCLineSegmentPair.Value as FTN.DCLineSegment;

                    ResourceDescription rd = CreateDCLineSegmentResourceDescription(cimDCLineSegment);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("DCLineSegment ID = ").Append(cimDCLineSegment.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("DCLineSegment ID = ").Append(cimDCLineSegment.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateDCLineSegmentResourceDescription(FTN.DCLineSegment cimDCLineSegment)
        {
            ResourceDescription rd = null;
            if (cimDCLineSegment != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.DCLINESEG, importHelper.CheckOutIndexForDMSType(DMSType.DCLINESEG));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimDCLineSegment.ID, gid);

                ////populate ResourceDescription
                LabsProfileConverter.PopulateDCLineSegmentProperties(cimDCLineSegment, rd, importHelper, report);
            }
            return rd;
        }

        private void ImportPerLengthSequenceImpedances()
        {
            SortedDictionary<string, object> cimPerLengthSequenceImpedances = concreteModel.GetAllObjectsOfType("FTN.PerLengthSequenceImpedance");
            if (cimPerLengthSequenceImpedances != null)
            {
                foreach (KeyValuePair<string, object> cimPerLengthSequenceImpedancePair in cimPerLengthSequenceImpedances)
                {
                    FTN.PerLengthSequenceImpedance cimPerLengthSequenceImpedance = cimPerLengthSequenceImpedancePair.Value as FTN.PerLengthSequenceImpedance;

                    ResourceDescription rd = CreatePerLengthSequenceImpedanceResourceDescription(cimPerLengthSequenceImpedance);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("PerLengthSequenceImpedance ID = ").Append(cimPerLengthSequenceImpedance.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("PerLengthSequenceImpedance ID = ").Append(cimPerLengthSequenceImpedance.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreatePerLengthSequenceImpedanceResourceDescription(FTN.PerLengthSequenceImpedance cimPerLengthSequenceImpedance)
        {
            ResourceDescription rd = null;
            if (cimPerLengthSequenceImpedance != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.PLENSEQIMPENDANCE, importHelper.CheckOutIndexForDMSType(DMSType.PLENSEQIMPENDANCE));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimPerLengthSequenceImpedance.ID, gid);

                ////populate ResourceDescription
                LabsProfileConverter.PopulatePerLengthSequenceImpedanceProperties(cimPerLengthSequenceImpedance, rd, importHelper, report);
            }
            return rd;
        }
        #endregion Import
    }
}

