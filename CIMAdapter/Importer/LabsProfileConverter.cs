namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	using FTN.Common;

    /// <summary>
    /// LabsProfileConverter has methods for populating
    /// ResourceDescription objects using LabsProfileConverterCIMProfile_Labs objects.
    /// </summary>
    public static class LabsProfileConverter
	{

		#region Populate ResourceDescription
		public static void PopulateIdentifiedObjectProperties(FTN.IdentifiedObject cimIdentifiedObject, ResourceDescription rd)
		{
			if ((cimIdentifiedObject != null) && (rd != null))
			{
				if (cimIdentifiedObject.MRIDHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_MRID, cimIdentifiedObject.MRID));
				}
				if (cimIdentifiedObject.NameHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_NAME, cimIdentifiedObject.Name));
				}
				if (cimIdentifiedObject.AliasNameHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_ALIASNAME, cimIdentifiedObject.AliasName));
				}
			}
		}

        public static void PopulateTerminalProperties(FTN.Terminal cimTerminal, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimTerminal != null) && (rd != null))
            {
                LabsProfileConverter.PopulateIdentifiedObjectProperties(cimTerminal, rd);

                if (cimTerminal.ConnectivityNodeHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimTerminal.ConnectivityNode.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimTerminal.GetType().ToString()).Append(" rdfID = \"").Append(cimTerminal.ID);
                        report.Report.Append("\" - Failed to set reference to Location: rdfID \"").Append(cimTerminal.ConnectivityNode.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.TERMINAL_CONNODE, gid));
                }

                if (cimTerminal.ConductingEquipmentHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimTerminal.ConductingEquipment.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimTerminal.GetType().ToString()).Append(" rdfID = \"").Append(cimTerminal.ID);
                        report.Report.Append("\" - Failed to set reference to Location: rdfID \"").Append(cimTerminal.ConductingEquipment.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.TERMINAL_CONDEQ, gid));
                }
            }
        }

        public static void PopulatePowerSystemResourceProperties(FTN.PowerSystemResource cimPowerSystemResource, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        { 
            if ((cimPowerSystemResource != null) && (rd != null))
            {
                LabsProfileConverter.PopulateIdentifiedObjectProperties(cimPowerSystemResource, rd);
            }
        }

        public static void PopulateConnectivityNodeProperties(FTN.ConnectivityNode cimConnectivityNode, ResourceDescription rd)
        {
            if ((cimConnectivityNode != null) && (rd != null))
            {
                LabsProfileConverter.PopulateIdentifiedObjectProperties(cimConnectivityNode, rd);

                if (cimConnectivityNode.DescriptionHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CONNODE_DESCRIPTION, cimConnectivityNode.Description));
                }
            }
        }

        public static void PopulateEquipmentProperties(FTN.Equipment cimEquipment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimEquipment != null) && (rd != null))
            {
                LabsProfileConverter.PopulatePowerSystemResourceProperties(cimEquipment, rd, importHelper, report);
            }
        }

        public static void PopulateConductingEquipmentProperties(FTN.ConductingEquipment cimConductingEquipment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimConductingEquipment != null) && (rd != null))
            {
                LabsProfileConverter.PopulateEquipmentProperties(cimConductingEquipment, rd, importHelper, report);
            }
        }

        public static void PopulateConductorProperties(FTN.Conductor cimConductor, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimConductor != null) && (rd != null))
            {
                LabsProfileConverter.PopulateConductingEquipmentProperties(cimConductor, rd, importHelper, report);

                if (cimConductor.LengthHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CONDUCTOR_LEN, cimConductor.Length));
                }
            }
        }

        public static void PopulateDCLineSegmentProperties(FTN.DCLineSegment cimDCLineSegment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimDCLineSegment != null) && (rd != null))
            {
                LabsProfileConverter.PopulateConductingEquipmentProperties(cimDCLineSegment, rd, importHelper, report);
            }
        }

        public static void PopulateACLineSegmentProperties(FTN.ACLineSegment cimACLineSegment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimACLineSegment != null) && (rd != null))
            {
                LabsProfileConverter.PopulateConductorProperties(cimACLineSegment, rd, importHelper, report);

                if (cimACLineSegment.B0chHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ACLINESEG_B0CH, cimACLineSegment.B0ch));
                }
                if (cimACLineSegment.BchHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ACLINESEG_BCH, cimACLineSegment.Bch));
                }
                if (cimACLineSegment.G0chHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ACLINESEG_G0CH, cimACLineSegment.G0ch));
                }
                if (cimACLineSegment.G0chHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ACLINESEG_GCH, cimACLineSegment.Gch));
                }
                if (cimACLineSegment.RHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ACLINESEG_R, cimACLineSegment.R));
                }
                if (cimACLineSegment.R0HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ACLINESEG_R0, cimACLineSegment.R0));
                }
                if (cimACLineSegment.XHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ACLINESEG_X, cimACLineSegment.X));
                }
                if (cimACLineSegment.X0HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ACLINESEG_X0, cimACLineSegment.X0));
                }

                if (cimACLineSegment.PerLengthImpedanceHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimACLineSegment.PerLengthImpedance.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimACLineSegment.GetType().ToString()).Append(" rdfID = \"").Append(cimACLineSegment.ID);
                        report.Report.Append("\" - Failed to set reference to PerLengthImpedance: rdfID \"").Append(cimACLineSegment.PerLengthImpedance.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.ACLINESEG_PERLENIMP, gid));
                }
            }
        }
        public static void PopulateSeriesCompensatorProperties(FTN.SeriesCompensator cimSeriesCompensator, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimSeriesCompensator != null) && (rd != null))
            {
                LabsProfileConverter.PopulateConductingEquipmentProperties(cimSeriesCompensator, rd, importHelper, report);

                if (cimSeriesCompensator.RHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SERCOMPENSATOR_R, cimSeriesCompensator.R));
                }
                if (cimSeriesCompensator.R0HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SERCOMPENSATOR_R0, cimSeriesCompensator.R0));
                }
                if (cimSeriesCompensator.XHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SERCOMPENSATOR_X, cimSeriesCompensator.X));
                }
                if (cimSeriesCompensator.X0HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SERCOMPENSATOR_X0, cimSeriesCompensator.X0));
                }
            }
        }

        public static void PopulatePerLengthImpedanceProperties(FTN.PerLengthImpedance cimPerLengthImpedance, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimPerLengthImpedance != null) && (rd != null))
            {
                LabsProfileConverter.PopulateIdentifiedObjectProperties(cimPerLengthImpedance, rd);
            }
        }

        public static void PopulatePerLengthSequenceImpedanceProperties(FTN.PerLengthSequenceImpedance cimPerLengthSequenceImpedance, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimPerLengthSequenceImpedance != null) && (rd != null))
            {
                LabsProfileConverter.PopulatePerLengthImpedanceProperties(cimPerLengthSequenceImpedance, rd, importHelper, report);

                if (cimPerLengthSequenceImpedance.B0chHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.PERLENSEQIMP_B0CH, cimPerLengthSequenceImpedance.B0ch));
                }
                if (cimPerLengthSequenceImpedance.BchHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.PERLENSEQIMP_BCH, cimPerLengthSequenceImpedance.Bch));
                }
                if (cimPerLengthSequenceImpedance.G0chHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.PERLENSEQIMP_G0CH, cimPerLengthSequenceImpedance.G0ch));
                }
                if (cimPerLengthSequenceImpedance.G0chHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.PERLENSEQIMP_GCH, cimPerLengthSequenceImpedance.Gch));
                }
                if (cimPerLengthSequenceImpedance.RHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.PERLENSEQIMP_R, cimPerLengthSequenceImpedance.R));
                }
                if (cimPerLengthSequenceImpedance.R0HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.PERLENSEQIMP_R0, cimPerLengthSequenceImpedance.R0));
                }
                if (cimPerLengthSequenceImpedance.XHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.PERLENSEQIMP_X, cimPerLengthSequenceImpedance.X));
                }
                if (cimPerLengthSequenceImpedance.X0HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.PERLENSEQIMP_X0, cimPerLengthSequenceImpedance.X0));
                }
            }
        }
        #endregion Populate ResourceDescription

        #region Enums convert
        //public static WindingConnection GetDMSWindingConnection(FTN.WindingConnection windingConnection)
        //{
        //	switch (windingConnection)
        //	{
        //		case FTN.WindingConnection.D:
        //			return WindingConnection.D;
        //		case FTN.WindingConnection.I:
        //			return WindingConnection.I;
        //		case FTN.WindingConnection.Z:
        //			return WindingConnection.Z;
        //		case FTN.WindingConnection.Y:
        //			return WindingConnection.Y;
        //		default:
        //			return WindingConnection.Y;
        //	}
        //}
        #endregion Enums convert
    }
}
