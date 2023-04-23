using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

/** 유저 정보 */
[JsonObject]
public class CUserInfo : CBaseInfo {
	#region 프로퍼티
	public string ID { get; set; } = string.Empty;
	public string PW { get; set; } = string.Empty;
	#endregion // 프로퍼티
}

/** 유저 정보 저장소 */
public class CUserInfoStorage : CSingleton<CUserInfoStorage> {
	#region 프로퍼티
	public CUserInfo UserInfo { get; private set; } = new CUserInfo();
	public string UserInfoPath => CAccess.GetWriteablePath("Datas/UserInfo.json");
	#endregion // 프로퍼티

	#region 함수
	/** 유저 정보를 로드한다 */
	public void LoadUserInfo() {
		// 유저 정보가 존재 할 경우
		if(File.Exists(this.UserInfoPath)) {
			this.UserInfo = CFunc.ReadObj<CUserInfo>(this.UserInfoPath);
		}
	}

	/** 유저 정보를 저장한다 */
	public void SaveUserInfo() {
		CFunc.WriteObj(this.UserInfo, this.UserInfoPath);
	}
	#endregion // 함수
}
