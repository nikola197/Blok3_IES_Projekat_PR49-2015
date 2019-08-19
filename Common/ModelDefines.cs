using System;
using System.Collections.Generic;
using System.Text;

namespace FTN.Common
{
	
	public enum DMSType : short
	{		
		MASK_TYPE		        = unchecked((short)0xFFFF),

        CONNODE                 = 0x0001,
        TERMINAL                = 0x0002,
        SERCOMPENSATOR          = 0x0003,
        DCLINESEG               = 0x0004,
        ACLINESEG               = 0x0005,
        PLENSEQIMPENDANCE       = 0x0006,
    }

    [Flags]
	public enum ModelCode : long
	{
        IDOBJ                   = 0x1000000000000000,
        IDOBJ_GID               = 0x1000000000000104,
        IDOBJ_ALIASNAME         = 0x1000000000000207,
        IDOBJ_MRID              = 0x1000000000000307,
        IDOBJ_NAME              = 0x1000000000000407,

        CONNODE                 = 0x1100000000010000,
        CONNODE_DESCRIPTION     = 0x1100000000010107,
        CONNODE_TERMINALS       = 0x1100000000010219,

        TERMINAL                = 0x1200000000020000,
        TERMINAL_CONNODE        = 0x1200000000020109,
        TERMINAL_CONDEQ         = 0x1200000000020209,

        POWSYSRESOURCE          = 0x1300000000000000,

        EQUIPMENT               = 0x1310000000000000,

        CONDEQ                  = 0x1311000000000000,
        CONDEQ_TERMINALS        = 0x1311000000000119,

        CONDUCTOR               = 0x1311100000000000,
        CONDUCTOR_LEN           = 0x1311100000000105,

        DCLINESEG               = 0x1311110000040000,

        ACLINESEG               = 0x1311120000050000,
        ACLINESEG_PERLENIMP     = 0x1311120000050109,
        ACLINESEG_B0CH          = 0x1311120000050205,
        ACLINESEG_BCH           = 0x1311120000050305,
        ACLINESEG_G0CH          = 0x1311120000050405,
        ACLINESEG_GCH           = 0x1311120000050505,
        ACLINESEG_R             = 0x1311120000050605,
        ACLINESEG_R0            = 0x1311120000050705,
        ACLINESEG_X             = 0x1311120000050805,
        ACLINESEG_X0            = 0x1311120000050905,

        PERLENIMP               = 0x1400000000000000,
        PERLENIMP_ACLINESEGS    = 0x1400000000000119,

        PERLENSEQIMP            = 0x1410000000060000,
        PERLENSEQIMP_B0CH       = 0x1410000000060105,
        PERLENSEQIMP_BCH        = 0x1410000000060205,
        PERLENSEQIMP_G0CH       = 0x1410000000060305,
        PERLENSEQIMP_GCH        = 0x1410000000060405,
        PERLENSEQIMP_R          = 0x1410000000060505,
        PERLENSEQIMP_R0         = 0x1410000000060605,
        PERLENSEQIMP_X          = 0x1410000000060705,
        PERLENSEQIMP_X0         = 0x1410000000060805,

        SERCOMPENSATOR          = 0x1311200000030000,
        SERCOMPENSATOR_R        = 0x1311200000030105,
        SERCOMPENSATOR_R0       = 0x1311200000030205,
        SERCOMPENSATOR_X        = 0x1311200000030305,
        SERCOMPENSATOR_X0       = 0x1311200000030405,

    }

    [Flags]
	public enum ModelCodeMask : long
	{
		MASK_TYPE			    = 0x00000000ffff0000,
		MASK_ATTRIBUTE_INDEX    = 0x000000000000ff00,
		MASK_ATTRIBUTE_TYPE	    = 0x00000000000000ff,

		MASK_INHERITANCE_ONLY   = unchecked((long)0xffffffff00000000),
		MASK_FIRSTNBL		    = unchecked((long)0xf000000000000000),
		MASK_DELFROMNBL8	    = unchecked((long)0xfffffff000000000),		
	}																		
}


