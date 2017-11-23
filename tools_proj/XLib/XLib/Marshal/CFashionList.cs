//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:2.0.50727.8784
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace XTable {
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    
    
    public class CFashionList {
        
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
		public struct RowData {
			uint itemid;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			string itemname;
			int equippos;
			int profession;
			int level;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			string quality;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			string directorycomment;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			string modelprefabwarrior;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			string modelprefabarcher;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			string modelprefabsorcer;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			string modelprefabcleric;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			string modelprefab5;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			string modelprefab6;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			string modelprefab7;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			string modelprefab8;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			string presentid;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
			int[] replaceid;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			string againreplaceid;

			public uint ItemID { get { return itemid; } }

			public string ItemName { get { return itemname; } }

			public int EquipPos { get { return equippos; } }

			public int Profession { get { return profession; } }

			public int Level { get { return level; } }

			public string Quality { get { return quality; } }

			public string DirectoryComment { get { return directorycomment; } }

			public string ModelPrefabWarrior { get { return modelprefabwarrior; } }

			public string ModelPrefabArcher { get { return modelprefabarcher; } }

			public string ModelPrefabSorcer { get { return modelprefabsorcer; } }

			public string ModelPrefabCleric { get { return modelprefabcleric; } }

			public string ModelPrefab5 { get { return modelprefab5; } }

			public string ModelPrefab6 { get { return modelprefab6; } }

			public string ModelPrefab7 { get { return modelprefab7; } }

			public string ModelPrefab8 { get { return modelprefab8; } }

			public string PresentID { get { return presentid; } }

			int[] Replaceid { 
				get { 
					if (replaceid.Length == 16) {
					List<int> list = new List<int>();
					for (int i = replaceid.Length - 1; i >= 0; i--)
					{
						if (replaceid[i] != -1) list.Add(replaceid[i]);
					}
					replaceid = list.ToArray();
					}
					 return replaceid;
				}
			}

			public string AgainReplaceID { get { return againreplaceid; } }
		}


#if UNITY_IPHONE || UNITY_XBOX360
		[DllImport("__Internal")]
#else
		[DllImport("XTable")]
#endif
		static extern void iGetFashionListRow(int idx, ref RowData row);

#if UNITY_IPHONE || UNITY_XBOX360
		[DllImport("__Internal")]
#else
		[DllImport("XTable")]
#endif
		static extern int iGetFashionListLength();
        
        private static RowData m_data;
        
        public static int length {
            get {
				return iGetFashionListLength();
            }
        }
        
        public static RowData GetRow(int idx) {
			iGetFashionListRow(idx, ref m_data);
			return m_data;
        }
    }
}
