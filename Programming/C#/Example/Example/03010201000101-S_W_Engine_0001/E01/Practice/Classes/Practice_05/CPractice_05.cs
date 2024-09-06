#define P05_01

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example._03010201000101_S_W_Engine_0001.E01.Practice.Classes.Practice__05
{
	internal class CPractice_05
	{
#if P05_01
		/** 무기 */
		private class CWeapon
		{
			public int LV { get; private set; } = 0;
		}

		/** 플레이어 */
		private class CPlayer
		{
			public int NumGolds { get; private set; } = 0;
			public List<CWeapon> WeaponList { get; private set; } = new List<CWeapon>();
		};
#endif // #if P05_01

		/** 초기화 */
		public static void Start(string[] args)
		{
#if P05_01
			/*
			 * 과제 5 - 1
			 * - 장비 상점 제작하기
			 * - 초기 금액을 설정 후 상점에서 물건을 사고 팔 수 있는 프로그램 제작하기
			 * - 모든 상품은 비용과 재고가 존재하면 현재 금액 또는 재고가 부족 할 경우 구입이 불가능하다
			 * - 판매 비용은 구입 비용의 70 %
			 * - 강화 장비 판매 비용은 판매 비용 + (구입 비용의 10 % x 강화 횟수)
			 * - 강화 비용은 구입 비용의 50 %
			 * - 최대 강화 횟수는 2 이다
			 * - 장비 강화 성공 확률은 50 % 이다
			 * - 장비 구입은 상점이 가지고 있는 장비를 출력하고 이외에는 플레이어 인벤토리에 있는 장비를 출력한다
			 * 
			 * Ex)
			 * 초기 금액 입력 : 1000
			 * 
			 * =====> 메뉴 <=====
			 * 1. 장비 구입
			 * 2. 장비 판매
			 * 3. 장비 강화
			 * 4. 종료
			 * 
			 * 메뉴 선택 : 1
			 * 
			 * =====> 구입 가능 장비 <=====
			 * 1. 숏 소드 Lv.1 x 3 (비용 : 150) 
			 * 2. 롱 소드 Lv.1 x 2 (비용 : 250)
			 * 3. 바스타드 소드 Lv.1 x 1 (비용 : 500)
			 * 4. 구입 종료
			 * 
			 * Case 1. 구입 가능 할 경우
			 * 구입 장비 선택 (현재 금액 : 1000) : 3
			 * 바스타드 소드를 구입했습니다.
			 * 
			 * Case 2. 금액 부족 or 재고 부족 일 경우
			 * 구입 장비 선택 (현재 금액 : 1000) : 3
			 * 바스타드 소드를 구입 할 수 없습니다.
			 * 
			 * 메뉴 선택 : 2
			 * 
			 * =====> 판매 가능 장비 <=====
			 * 1. 숏 소드 Lv.1 (비용 : 105)
			 * 2. 숏 소드 Lv.2 (비용 : 120)
			 * 3. 바스타스 소드 Lv.3 (비용 : 450)
			 * 4. 판매 종료
			 * 
			 * 판매 장비 선택 (현재 금액 : 1000) : 1
			 * 숏 소드를 판매했습니다.
			 * 
			 * 메뉴 선택 : 3
			 * 
			 * =====> 강화 가능 장비 <=====
			 * 1. 숏 소드 Lv.1 (비용 : 75)
			 * 2. 숏 소드 Lv.2 (비용 : 75)
			 * 3. 숏 소드 Lv.3 (강화 불가)
			 * 4. 강화 종료
			 * 
			 * Case 1. 강화에 성공했을 경우
			 * 강화 장비 선택 (현재 금액 : 1000) : 1
			 * 숏 소드 강화에 성공했습니다
			 * 
			 * Case 2. 강화에 실패했을 경우
			 * 강화 장비 선택 (현재 금액 : 1000) : 1
			 * 숏 소드 강화에 실패했습니다
			 * 
			 * Case 3. 금액 부족 or 강화가 불가능 할 경우
			 * 숏 소드를 강화 할 수 없습니다
			 */
#endif // #if P05_01
		}
	}
}
