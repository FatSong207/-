
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yuebon.Chaochi.Models
{
    /// <summary>
    /// 表格B030101，數據實體對象
    /// </summary>
    [Table("Chaochi_TBNoB3")]
    [Serializable]
    public class TBNoB3 : BaseEntity<string>, ICreationAudited, IModificationAudited
    { 
        /// <summary>
        /// 默認構造函數（需要初始化屬性的在此處理）
        /// </summary>
	    public TBNoB3()
        {
            
        }

        #region Property Members
        /// <summary>
        /// 表單編號
        /// </summary>
        public virtual string FName { get; set; }

        /// <summary>
        /// 由LNID、LCID、RNID、RCID、BAdd組成的值用於取得對應BZ欄位
        /// </summary>
        public virtual string RefKeys { get; set; }

        #region 租屋公司基本資料

        /// <summary>
        /// 公司名稱
        /// </summary>
        public virtual string ROName { get; set; }
        public virtual string RORep { get; set; }
        public virtual string ROAdd { get; set; }
        public virtual string ROUserName { get; set; }
        public virtual string ROTel { get; set; }

        /// <summary>
        /// 租屋公司傳真
        /// </summary>
        public virtual string ROFax { get; set; }
        public virtual string ROLRNo { get; set; }
        public virtual string ROID { get; set; }
        public virtual string RHMName { get; set; }
        public virtual string RHMRNo { get; set; }

        public virtual string AGName { get; set; }
        public virtual string AGLRNo { get; set; }
        public virtual string AGTel { get; set; }

        #endregion 租屋公司基本資料

        /// <summary>
        /// BZ001
        /// </summary>
        public virtual string BZ001 { get; set; }

        /// <summary>
        /// BZ002
        /// </summary>
        public virtual string BZ002 { get; set; }

        /// <summary>
        /// BZ003
        /// </summary>
        public virtual string BZ003 { get; set; }

        /// <summary>
        /// BZ004
        /// </summary>
        public virtual string BZ004 { get; set; }

        /// <summary>
        /// BZ005
        /// </summary>
        public virtual string BZ005 { get; set; }

        /// <summary>
        /// BZ006
        /// </summary>
        public virtual string BZ006 { get; set; }

        /// <summary>
        /// BZ007
        /// </summary>
        public virtual string BZ007 { get; set; }

        /// <summary>
        /// BZ008
        /// </summary>
        public virtual string BZ008 { get; set; }

        /// <summary>
        /// BZ009
        /// </summary>
        public virtual string BZ009 { get; set; }

        /// <summary>
        /// BZ010
        /// </summary>
        public virtual string BZ010 { get; set; }

        /// <summary>
        /// BZ011
        /// </summary>
        public virtual string BZ011 { get; set; }

        /// <summary>
        /// BZ012
        /// </summary>
        public virtual string BZ012 { get; set; }

        /// <summary>
        /// BZ013
        /// </summary>
        public virtual string BZ013 { get; set; }

        /// <summary>
        /// BZ014
        /// </summary>
        public virtual string BZ014 { get; set; }

        /// <summary>
        /// BZ015
        /// </summary>
        public virtual string BZ015 { get; set; }

        /// <summary>
        /// BZ016
        /// </summary>
        public virtual string BZ016 { get; set; }

        /// <summary>
        /// BZ017
        /// </summary>
        public virtual string BZ017 { get; set; }

        /// <summary>
        /// BZ018
        /// </summary>
        public virtual string BZ018 { get; set; }

        /// <summary>
        /// BZ019
        /// </summary>
        public virtual string BZ019 { get; set; }

        /// <summary>
        /// BZ020
        /// </summary>
        public virtual string BZ020 { get; set; }

        /// <summary>
        /// BZ021
        /// </summary>
        public virtual string BZ021 { get; set; }

        /// <summary>
        /// BZ022
        /// </summary>
        public virtual string BZ022 { get; set; }

        /// <summary>
        /// BZ023
        /// </summary>
        public virtual string BZ023 { get; set; }

        /// <summary>
        /// BZ024
        /// </summary>
        public virtual string BZ024 { get; set; }

        /// <summary>
        /// BZ025
        /// </summary>
        public virtual string BZ025 { get; set; }

        /// <summary>
        /// BZ026
        /// </summary>
        public virtual string BZ026 { get; set; }

        /// <summary>
        /// BZ027
        /// </summary>
        public virtual string BZ027 { get; set; }

        /// <summary>
        /// BZ028
        /// </summary>
        public virtual string BZ028 { get; set; }

        /// <summary>
        /// BZ029
        /// </summary>
        public virtual string BZ029 { get; set; }

        /// <summary>
        /// BZ030
        /// </summary>
        public virtual string BZ030 { get; set; }

        /// <summary>
        /// BZ031
        /// </summary>
        public virtual string BZ031 { get; set; }

        /// <summary>
        /// BZ032
        /// </summary>
        public virtual string BZ032 { get; set; }

        /// <summary>
        /// BZ033
        /// </summary>
        public virtual string BZ033 { get; set; }

        /// <summary>
        /// BZ034
        /// </summary>
        public virtual string BZ034 { get; set; }

        /// <summary>
        /// BZ035
        /// </summary>
        public virtual string BZ035 { get; set; }

        /// <summary>
        /// BZ036
        /// </summary>
        public virtual string BZ036 { get; set; }

        /// <summary>
        /// BZ037
        /// </summary>
        public virtual string BZ037 { get; set; }

        /// <summary>
        /// BZ038
        /// </summary>
        public virtual string BZ038 { get; set; }

        /// <summary>
        /// BZ039
        /// </summary>
        public virtual string BZ039 { get; set; }

        /// <summary>
        /// BZ040
        /// </summary>
        public virtual string BZ040 { get; set; }

        /// <summary>
        /// BZ041
        /// </summary>
        public virtual string BZ041 { get; set; }

        /// <summary>
        /// BZ042
        /// </summary>
        public virtual string BZ042 { get; set; }

        /// <summary>
        /// BZ043
        /// </summary>
        public virtual string BZ043 { get; set; }

        /// <summary>
        /// BZ044
        /// </summary>
        public virtual string BZ044 { get; set; }

        /// <summary>
        /// BZ045
        /// </summary>
        public virtual string BZ045 { get; set; }

        /// <summary>
        /// BZ046
        /// </summary>
        public virtual string BZ046 { get; set; }

        /// <summary>
        /// BZ047
        /// </summary>
        public virtual string BZ047 { get; set; }

        /// <summary>
        /// BZ048
        /// </summary>
        public virtual string BZ048 { get; set; }

        /// <summary>
        /// BZ049
        /// </summary>
        public virtual string BZ049 { get; set; }

        /// <summary>
        /// BZ050
        /// </summary>
        public virtual string BZ050 { get; set; }

        /// <summary>
        /// BZ051
        /// </summary>
        public virtual string BZ051 { get; set; }

        /// <summary>
        /// BZ052
        /// </summary>
        public virtual string BZ052 { get; set; }

        /// <summary>
        /// BZ053
        /// </summary>
        public virtual string BZ053 { get; set; }

        /// <summary>
        /// BZ054
        /// </summary>
        public virtual string BZ054 { get; set; }

        /// <summary>
        /// BZ055
        /// </summary>
        public virtual string BZ055 { get; set; }

        /// <summary>
        /// BZ056
        /// </summary>
        public virtual string BZ056 { get; set; }

        /// <summary>
        /// BZ057
        /// </summary>
        public virtual string BZ057 { get; set; }

        /// <summary>
        /// BZ058
        /// </summary>
        public virtual string BZ058 { get; set; }

        /// <summary>
        /// BZ059
        /// </summary>
        public virtual string BZ059 { get; set; }

        /// <summary>
        /// BZ060
        /// </summary>
        public virtual string BZ060 { get; set; }

        /// <summary>
        /// BZ061
        /// </summary>
        public virtual string BZ061 { get; set; }

        /// <summary>
        /// BZ062
        /// </summary>
        public virtual string BZ062 { get; set; }

        /// <summary>
        /// BZ063
        /// </summary>
        public virtual string BZ063 { get; set; }

        /// <summary>
        /// BZ064
        /// </summary>
        public virtual string BZ064 { get; set; }

        /// <summary>
        /// BZ065
        /// </summary>
        public virtual string BZ065 { get; set; }

        /// <summary>
        /// BZ066
        /// </summary>
        public virtual string BZ066 { get; set; }

        /// <summary>
        /// BZ067
        /// </summary>
        public virtual string BZ067 { get; set; }

        /// <summary>
        /// BZ068
        /// </summary>
        public virtual string BZ068 { get; set; }

        /// <summary>
        /// BZ069
        /// </summary>
        public virtual string BZ069 { get; set; }

        /// <summary>
        /// BZ070
        /// </summary>
        public virtual string BZ070 { get; set; }

        /// <summary>
        /// BZ071
        /// </summary>
        public virtual string BZ071 { get; set; }

        /// <summary>
        /// BZ072
        /// </summary>
        public virtual string BZ072 { get; set; }

        /// <summary>
        /// BZ073
        /// </summary>
        public virtual string BZ073 { get; set; }

        /// <summary>
        /// BZ074
        /// </summary>
        public virtual string BZ074 { get; set; }

        /// <summary>
        /// BZ075
        /// </summary>
        public virtual string BZ075 { get; set; }

        /// <summary>
        /// BZ076
        /// </summary>
        public virtual string BZ076 { get; set; }

        /// <summary>
        /// BZ077
        /// </summary>
        public virtual string BZ077 { get; set; }

        /// <summary>
        /// BZ078
        /// </summary>
        public virtual string BZ078 { get; set; }

        /// <summary>
        /// BZ079
        /// </summary>
        public virtual string BZ079 { get; set; }

        /// <summary>
        /// BZ080
        /// </summary>
        public virtual string BZ080 { get; set; }

        /// <summary>
        /// BZ081
        /// </summary>
        public virtual string BZ081 { get; set; }

        /// <summary>
        /// BZ082
        /// </summary>
        public virtual string BZ082 { get; set; }

        /// <summary>
        /// BZ083
        /// </summary>
        public virtual string BZ083 { get; set; }

        /// <summary>
        /// BZ084
        /// </summary>
        public virtual string BZ084 { get; set; }

        /// <summary>
        /// BZ085
        /// </summary>
        public virtual string BZ085 { get; set; }

        /// <summary>
        /// BZ086
        /// </summary>
        public virtual string BZ086 { get; set; }

        /// <summary>
        /// BZ087
        /// </summary>
        public virtual string BZ087 { get; set; }

        /// <summary>
        /// BZ088
        /// </summary>
        public virtual string BZ088 { get; set; }

        /// <summary>
        /// BZ089
        /// </summary>
        public virtual string BZ089 { get; set; }

        /// <summary>
        /// BZ090
        /// </summary>
        public virtual string BZ090 { get; set; }

        /// <summary>
        /// BZ091
        /// </summary>
        public virtual string BZ091 { get; set; }

        /// <summary>
        /// BZ092
        /// </summary>
        public virtual string BZ092 { get; set; }

        /// <summary>
        /// BZ093
        /// </summary>
        public virtual string BZ093 { get; set; }

        /// <summary>
        /// BZ094
        /// </summary>
        public virtual string BZ094 { get; set; }

        /// <summary>
        /// BZ095
        /// </summary>
        public virtual string BZ095 { get; set; }

        /// <summary>
        /// BZ096
        /// </summary>
        public virtual string BZ096 { get; set; }

        /// <summary>
        /// BZ097
        /// </summary>
        public virtual string BZ097 { get; set; }

        /// <summary>
        /// BZ098
        /// </summary>
        public virtual string BZ098 { get; set; }

        /// <summary>
        /// BZ099
        /// </summary>
        public virtual string BZ099 { get; set; }

        /// <summary>
        /// BZ100
        /// </summary>
        public virtual string BZ100 { get; set; }

        /// <summary>
        /// BZ101
        /// </summary>
        public virtual string BZ101 { get; set; }

        /// <summary>
        /// BZ102
        /// </summary>
        public virtual string BZ102 { get; set; }

        /// <summary>
        /// BZ103
        /// </summary>
        public virtual string BZ103 { get; set; }

        /// <summary>
        /// BZ104
        /// </summary>
        public virtual string BZ104 { get; set; }

        /// <summary>
        /// BZ105
        /// </summary>
        public virtual string BZ105 { get; set; }

        /// <summary>
        /// BZ106
        /// </summary>
        public virtual string BZ106 { get; set; }

        /// <summary>
        /// BZ107
        /// </summary>
        public virtual string BZ107 { get; set; }

        /// <summary>
        /// BZ108
        /// </summary>
        public virtual string BZ108 { get; set; }

        /// <summary>
        /// BZ109
        /// </summary>
        public virtual string BZ109 { get; set; }

        /// <summary>
        /// BZ110
        /// </summary>
        public virtual string BZ110 { get; set; }

        /// <summary>
        /// BZ111
        /// </summary>
        public virtual string BZ111 { get; set; }

        /// <summary>
        /// BZ112
        /// </summary>
        public virtual string BZ112 { get; set; }

        /// <summary>
        /// BZ113
        /// </summary>
        public virtual string BZ113 { get; set; }

        /// <summary>
        /// BZ114
        /// </summary>
        public virtual string BZ114 { get; set; }

        /// <summary>
        /// BZ115
        /// </summary>
        public virtual string BZ115 { get; set; }

        /// <summary>
        /// BZ116
        /// </summary>
        public virtual string BZ116 { get; set; }

        /// <summary>
        /// BZ117
        /// </summary>
        public virtual string BZ117 { get; set; }

        /// <summary>
        /// BZ118
        /// </summary>
        public virtual string BZ118 { get; set; }

        /// <summary>
        /// BZ119
        /// </summary>
        public virtual string BZ119 { get; set; }

        /// <summary>
        /// BZ120
        /// </summary>
        public virtual string BZ120 { get; set; }

        /// <summary>
        /// BZ121
        /// </summary>
        public virtual string BZ121 { get; set; }

        /// <summary>
        /// BZ122
        /// </summary>
        public virtual string BZ122 { get; set; }

        /// <summary>
        /// BZ123
        /// </summary>
        public virtual string BZ123 { get; set; }

        /// <summary>
        /// BZ124
        /// </summary>
        public virtual string BZ124 { get; set; }

        /// <summary>
        /// BZ125
        /// </summary>
        public virtual string BZ125 { get; set; }

        /// <summary>
        /// BZ126
        /// </summary>
        public virtual string BZ126 { get; set; }

        /// <summary>
        /// BZ127
        /// </summary>
        public virtual string BZ127 { get; set; }

        /// <summary>
        /// BZ128
        /// </summary>
        public virtual string BZ128 { get; set; }

        /// <summary>
        /// BZ129
        /// </summary>
        public virtual string BZ129 { get; set; }

        /// <summary>
        /// BZ130
        /// </summary>
        public virtual string BZ130 { get; set; }

        /// <summary>
        /// BZ131
        /// </summary>
        public virtual string BZ131 { get; set; }

        /// <summary>
        /// BZ132
        /// </summary>
        public virtual string BZ132 { get; set; }

        /// <summary>
        /// BZ133
        /// </summary>
        public virtual string BZ133 { get; set; }

        /// <summary>
        /// BZ134
        /// </summary>
        public virtual string BZ134 { get; set; }

        /// <summary>
        /// BZ135
        /// </summary>
        public virtual string BZ135 { get; set; }

        /// <summary>
        /// BZ136
        /// </summary>
        public virtual string BZ136 { get; set; }

        /// <summary>
        /// BZ137
        /// </summary>
        public virtual string BZ137 { get; set; }

        /// <summary>
        /// BZ138
        /// </summary>
        public virtual string BZ138 { get; set; }

        /// <summary>
        /// BZ139
        /// </summary>
        public virtual string BZ139 { get; set; }

        /// <summary>
        /// BZ140
        /// </summary>
        public virtual string BZ140 { get; set; }

        /// <summary>
        /// BZ141
        /// </summary>
        public virtual string BZ141 { get; set; }

        /// <summary>
        /// BZ142
        /// </summary>
        public virtual string BZ142 { get; set; }

        /// <summary>
        /// BZ143
        /// </summary>
        public virtual string BZ143 { get; set; }

        /// <summary>
        /// BZ144
        /// </summary>
        public virtual string BZ144 { get; set; }

        /// <summary>
        /// BZ145
        /// </summary>
        public virtual string BZ145 { get; set; }

        /// <summary>
        /// BZ146
        /// </summary>
        public virtual string BZ146 { get; set; }

        /// <summary>
        /// BZ147
        /// </summary>
        public virtual string BZ147 { get; set; }

        /// <summary>
        /// BZ148
        /// </summary>
        public virtual string BZ148 { get; set; }

        /// <summary>
        /// BZ149
        /// </summary>
        public virtual string BZ149 { get; set; }

        /// <summary>
        /// BZ150
        /// </summary>
        public virtual string BZ150 { get; set; }

        /// <summary>
        /// BZ151
        /// </summary>
        public virtual string BZ151 { get; set; }

        /// <summary>
        /// BZ152
        /// </summary>
        public virtual string BZ152 { get; set; }

        /// <summary>
        /// BZ153
        /// </summary>
        public virtual string BZ153 { get; set; }

        /// <summary>
        /// BZ154
        /// </summary>
        public virtual string BZ154 { get; set; }

        /// <summary>
        /// BZ155
        /// </summary>
        public virtual string BZ155 { get; set; }

        /// <summary>
        /// BZ156
        /// </summary>
        public virtual string BZ156 { get; set; }

        /// <summary>
        /// BZ157
        /// </summary>
        public virtual string BZ157 { get; set; }

        /// <summary>
        /// BZ158
        /// </summary>
        public virtual string BZ158 { get; set; }

        /// <summary>
        /// BZ159
        /// </summary>
        public virtual string BZ159 { get; set; }

        /// <summary>
        /// BZ160
        /// </summary>
        public virtual string BZ160 { get; set; }

        /// <summary>
        /// BZ161
        /// </summary>
        public virtual string BZ161 { get; set; }

        /// <summary>
        /// BZ162
        /// </summary>
        public virtual string BZ162 { get; set; }

        /// <summary>
        /// BZ163
        /// </summary>
        public virtual string BZ163 { get; set; }

        /// <summary>
        /// BZ164
        /// </summary>
        public virtual string BZ164 { get; set; }

        /// <summary>
        /// BZ165
        /// </summary>
        public virtual string BZ165 { get; set; }

        /// <summary>
        /// BZ166
        /// </summary>
        public virtual string BZ166 { get; set; }

        /// <summary>
        /// BZ167
        /// </summary>
        public virtual string BZ167 { get; set; }

        /// <summary>
        /// BZ168
        /// </summary>
        public virtual string BZ168 { get; set; }

        /// <summary>
        /// BZ169
        /// </summary>
        public virtual string BZ169 { get; set; }

        /// <summary>
        /// BZ170
        /// </summary>
        public virtual string BZ170 { get; set; }

        /// <summary>
        /// BZ171
        /// </summary>
        public virtual string BZ171 { get; set; }

        /// <summary>
        /// BZ172
        /// </summary>
        public virtual string BZ172 { get; set; }

        /// <summary>
        /// BZ173
        /// </summary>
        public virtual string BZ173 { get; set; }

        /// <summary>
        /// BZ174
        /// </summary>
        public virtual string BZ174 { get; set; }

        /// <summary>
        /// BZ175
        /// </summary>
        public virtual string BZ175 { get; set; }

        /// <summary>
        /// BZ176
        /// </summary>
        public virtual string BZ176 { get; set; }

        /// <summary>
        /// BZ177
        /// </summary>
        public virtual string BZ177 { get; set; }

        /// <summary>
        /// BZ178
        /// </summary>
        public virtual string BZ178 { get; set; }

        /// <summary>
        /// BZ179
        /// </summary>
        public virtual string BZ179 { get; set; }

        /// <summary>
        /// BZ180
        /// </summary>
        public virtual string BZ180 { get; set; }

        /// <summary>
        /// BZ181
        /// </summary>
        public virtual string BZ181 { get; set; }

        /// <summary>
        /// BZ182
        /// </summary>
        public virtual string BZ182 { get; set; }

        /// <summary>
        /// BZ183
        /// </summary>
        public virtual string BZ183 { get; set; }

        /// <summary>
        /// BZ184
        /// </summary>
        public virtual string BZ184 { get; set; }

        /// <summary>
        /// BZ185
        /// </summary>
        public virtual string BZ185 { get; set; }

        /// <summary>
        /// BZ186
        /// </summary>
        public virtual string BZ186 { get; set; }

        /// <summary>
        /// BZ187
        /// </summary>
        public virtual string BZ187 { get; set; }

        /// <summary>
        /// BZ188
        /// </summary>
        public virtual string BZ188 { get; set; }

        /// <summary>
        /// BZ189
        /// </summary>
        public virtual string BZ189 { get; set; }

        /// <summary>
        /// BZ190
        /// </summary>
        public virtual string BZ190 { get; set; }

        /// <summary>
        /// BZ191
        /// </summary>
        public virtual string BZ191 { get; set; }

        /// <summary>
        /// BZ192
        /// </summary>
        public virtual string BZ192 { get; set; }

        /// <summary>
        /// BZ193
        /// </summary>
        public virtual string BZ193 { get; set; }

        /// <summary>
        /// BZ194
        /// </summary>
        public virtual string BZ194 { get; set; }

        /// <summary>
        /// BZ195
        /// </summary>
        public virtual string BZ195 { get; set; }

        /// <summary>
        /// BZ196
        /// </summary>
        public virtual string BZ196 { get; set; }

        /// <summary>
        /// BZ197
        /// </summary>
        public virtual string BZ197 { get; set; }

        /// <summary>
        /// BZ198
        /// </summary>
        public virtual string BZ198 { get; set; }

        /// <summary>
        /// BZ199
        /// </summary>
        public virtual string BZ199 { get; set; }

        /// <summary>
        /// BZ200
        /// </summary>
        public virtual string BZ200 { get; set; }

        /// <summary>
        /// BZ201
        /// </summary>
        public virtual string BZ201 { get; set; }

        /// <summary>
        /// BZ202
        /// </summary>
        public virtual string BZ202 { get; set; }

        /// <summary>
        /// BZ203
        /// </summary>
        public virtual string BZ203 { get; set; }

        /// <summary>
        /// BZ204
        /// </summary>
        public virtual string BZ204 { get; set; }

        /// <summary>
        /// BZ205
        /// </summary>
        public virtual string BZ205 { get; set; }

        /// <summary>
        /// BZ206
        /// </summary>
        public virtual string BZ206 { get; set; }

        /// <summary>
        /// BZ207
        /// </summary>
        public virtual string BZ207 { get; set; }

        /// <summary>
        /// BZ208
        /// </summary>
        public virtual string BZ208 { get; set; }

        /// <summary>
        /// BZ209
        /// </summary>
        public virtual string BZ209 { get; set; }

        /// <summary>
        /// BZ210
        /// </summary>
        public virtual string BZ210 { get; set; }

        /// <summary>
        /// 創建日期
        /// </summary>
        public virtual DateTime? CreatorTime { get; set; }

        /// <summary>
        /// 創建用戶主鍵
        /// </summary>
        [MaxLength(50)]
        public virtual string CreatorUserId { get; set; }

        /// <summary>
        /// 最後修改時間
        /// </summary>
        public virtual DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最後修改用戶
        /// </summary>
        [MaxLength(50)]
        public virtual string LastModifyUserId { get; set; }
        #endregion

    }
}