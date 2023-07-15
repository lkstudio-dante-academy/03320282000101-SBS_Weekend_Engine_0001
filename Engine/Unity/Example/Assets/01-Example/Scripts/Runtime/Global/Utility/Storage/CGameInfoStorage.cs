using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

/** 게임 정보 */
[JsonObject]
public class CGameInfo : CBaseInfo {
	#region 프로퍼티
	public bool IsMuteBGSnd { get; set; } = false;
	public bool IsMuteFXSnds { get; set; } = false;
	#endregion // 프로퍼티
}

/** 게임 정보 저장소 */
public class CGameInfoStorage : CSingleton<CGameInfoStorage> {
	#region 프로퍼티
	public CGameInfo GameInfo { get; private set; } = new CGameInfo();
	public string GameInfoPath => CAccess.GetWriteablePath("Datas/GameInfo.json");
	#endregion // 프로퍼티

	#region 함수
	/** 게임 정보를 로드한다 */
	public void LoadGameInfo() {
		// 게임 정보가 존재 할 경우
		if(File.Exists(this.GameInfoPath)) {
			this.GameInfo = CFunc.ReadObj<CGameInfo>(this.GameInfoPath);
		}
	}

	/** 게임 정보를 저장한다 */
	public void SaveGameInfo() {
		CFunc.WriteObj(this.GameInfo, this.GameInfoPath);
	}
	#endregion // 함수
}
