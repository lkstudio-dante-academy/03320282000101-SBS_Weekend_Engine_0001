//#define E08_CLASS_01
#define E08_CLASS_02
#define E08_CLASS_03

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 클래스란?
 * - 데이터와 메서드의 집합을 표현하는 사용자 정의 자료형을 의미한다. (즉, 클래스를 활용하면 객체 지향 프로그래밍에서
 * 핵심이 되는 사물 (객체) 을 표현하는 것이 가능하다.)
 * 
 * 클래스는 객체 지향 프로그래밍에서 핵심이 되는 도구로써 특정 사물의 특징 (속성) 과 행위 (기능) 를 변수와 메서드로
 * 표현함으로써 사물 간에 관계를 통해 프로그램 구조를 설계하는 것이 가능하다.
 * 
 * C# 클래스 정의 방법
 * - class + 클래스 이름 + 클래스 맴버 (변수, 메서드)
 * 
 * Ex)
 * class CPCharacter {
 *		private int m_nHP;
 *		private int m_nLV;
 *		private int m_nATK;
 *		
 *		// 공격한다
 *		public void Attack(CMonster a_oTarget);
 *		
 *		// 점프한다
 *		public void Jump(Vector3 a_stVelocity);
 *		
 *		// 아이템을 획득한다
 *		public void GetItem(CItem a_oTarget);
 * }
 * 
 * CPCharacter oCharacterA = new CPCharacter();
 * CPCharacter oCharacterB = oCharacterA;
 * 
 * 상속이란?
 * - 특정 클래스가 지니고 있는 특징 (변수, 메서드) 를 물려주는 기능을 의미한다. (즉, 상속을 활용하면 특정 클래스가 지니고
 * 있는 기능을 확장하는 것이 가능하다.)
 * 
 * 또한, 특정 클래스를 상속했을 경우 해당 클래스와는 부모/자식의 관계를 형성하게 된다. 따라서, 자식 클래스는 부모 클래스가
 * 지니고 있는 변수와 메서드를 사용하는 것이 가능하다.
 * 
 * 단, C# 은 상속 할 부모 클래스를 하나만 지정 할 수 있다. (즉, 여러 부모 클래스를 지정하는 다중 상속을 C# 은 지원하지
 * 않는다.)
 */
namespace Example.Classes.Example_09 {
	internal class CExample_09 {
#if E08_CLASS_01
		/*
		 * 접근 제어 지시자란?
		 * - 클래스 맴버 (변수, 메서드) 에 접근 할 수 있는 범위를 제한 할 수 있는 키워드를 의미한다. (즉, 접근 제어 지시자를
		 * 활용하면 클래스의 특정 맴버를 좀 더 안정하게 제어하는 것이 가능하다.)
		 * 
		 * 객체 지향 프로그래밍에서는 정보 은닉 (캡슐화), 상속, 다형성이라고하는 3 가지 요소가 존재하며 접근 제어 지시자는
		 * 정보 은닉과 연관되어 있다.
		 * 
		 * 즉, 정보 은닉은 클래스 맴버의 범위를 제한함으로써 특정 클래스를 제어 할 수 있는 수단을 제어하는 것을 의미한다.
		 * 
		 * 따라서, 정보 은닉 개념을 활용하면 외부에 노출하기 민감한 데이터를 좀 더 제한 된 방식으로 제어함으로써 안정성을
		 * 높히는 것이 가능하다.
		 * 
		 * C# 접근 제어 지시자 (한정자) 종류
		 * - public			<- 클래스 내부/외부에서 모두 접근 가능
		 * - private		<- 클래스 내부에서만 접근 가능
		 * - protected		<- 클래스 내부와 자식 클래스에서만 접근 가능
		 * 
		 * 객체 지향 프로그래밍에서 맴버 변수는 private, 맴버 메서드는 public 으로 제한하는 것이 일반적이 관례이다. 따라서,
		 * 클래스 외부에서 특정 객체의 맴버에 접근하기 위해서는 해당 역할을 수행하는 메서드를 별도로 구현해야하며 해당 메서드는
		 * 접근자 메서드라고 지칭된다. (즉, 특정 맴버 변수의 데이터를 가져오는 메서드는 Getter, 특정 맴버 변수의 데이터를
		 * 변경하는 것은 Setter 라고 한다.)
		 * 
		 * 또한, 클래스 맴버에 별도로 접근 제어 지시자를 명시하지 않을 경우 컴파일러에 의해서 자동으로 private 수준으로 지정
		 * 되는 특징이 존재한다.
		 */
		/** 플레이어블 캐릭터 */
		class CPCharacter {
			private int m_nLV;
			private int m_nHP;

			private string m_oName;

			/*
			 * 자동 구현 프로퍼티를 활용하면 프로퍼티를 좀 더 수월하게 구현하는 것이 가능하다. (즉, 자동 구현 프로퍼티를
			 * 활용하면 컴파일러에 의해서 맴버 변수가 자동으로 선언되기 때문에 외부에 공개 할 맴버 변수를 좀 더 수월하게
			 * 선언 및 제어하는 것이 가능하다.)
			 */
			public string ID { get; private set; }

			/*
			 * 프로퍼티란?
			 * - 클래스의 특정 맴버 변수에 접근하기 위해서 C# 에서 제공하는 기능을 의미한다. 객체 지향 프로그래밍에서
			 * 정통적인 방식으로는 특정 맴버 변수를 제어하기 위해서 접근자 함수 (메서드) 를 구현하는 것이 일반적인 관례이지만
			 * C# 에서는 프로퍼티를 활용함으로써 불필요한 접근자 함수 구현을 방지하는 것이 가능하다.
			 */
			public int LV {
				get {
					return m_nLV;
				} set {
					m_nLV = value;
				}
			}

			public int HP {
				get {
					return m_nHP;
				}
				set {
					m_nHP = value;
				}
			}

			public string Name {
				get {
					return m_oName;
				}
				set {
					m_oName = value;
				}
			}

			/*
			 * 생성자란?
			 * - 객체가 생성 될 때 가장 처음 호출되는 메서드를 의미한다. (즉, 해당 생성자는 프로그래머가 명시적으로 호출
			 * 하는 것이 불가능하며 생성자는 객체가 생성 될 때 컴파일러에 의해서 자동으로 호출 된다는 것을 알 수 있다.)
			 * 
			 * 따라서, 생성자를 활용하면 객체를 생성과 동시에 특정 데이터로 맴버 변수를 초기화하는 것이 가능하다.
			 * 
			 * 또한, 객체가 생성되기 위해서는 반드시 생성자가 호출되어야하기 때문에 특정 클래스에 생성자가 존재하지 않을
			 * 경우 컴파일러에 의해서 자동으로 기본 생성자가 구현되는 특징이 존재한다.
			 * 
			 * 단, 프로그래머가 별도의 생성자를 구현했을 경우에는 컴파일러가 더이상 기본 생성자를 자동으로 구현해주지 않기
			 * 때문에 만약 기본 생성자가 필요 할 경우 명시적으로 구현해줘야한다.
			 */
			/** 생성자 */
			public CPCharacter() {
				// Do Something
			}

			/*
			 * 위임 생성자란?
			 * - 생성자 내부에서 다른 생성자를 호출 할 수 있는 기능을 의미한다. (즉, 위임 생성자를 활용하면 특정 객체를
			 * 초기화하는 과정을 통일 시키는 것이 가능하다.)
			 * 
			 * 따라서, 위임 생성자를 활용하면 객체가 생성되는 과정에서 발생하는 중복을 최소화하는 것이 가능하다.
			 * 
			 * default 키워드란?
			 * - default 키워드는 특정 자료형의 기본 값을 의미하며, 해당 키워드를 활용하면 변수를 선언 할 때 C# 내부에
			 * 설정되어있는 기본 값으로 설정하는 것이 가능하다.
			 * 
			 * 또한, C# 생성자는 맴버 변수를 default 값으로 설정하는 특징이 존재하기 때문에 특정 객체를 생성 후 아무 데이터도
			 * 설정하지 않았을 경우 자동으로 default 값으로 설정 된다는 것을 알 수 있다.
			 */
			/** 생성자 */
			public CPCharacter(int a_nLV, int a_nHP) : this(a_nLV, a_nHP, "Unknown") {
				// Do Something
			}

			/** 생성자 */
			public CPCharacter(int a_nLV, int a_nHP, string a_oName) {
				m_nLV = a_nLV;
				m_nHP = a_nHP;
				m_oName = a_oName;
			}

			/** 레벨을 반환한다 */
			public int GetLV() {
				return m_nLV;
			}

			/** 체력을 반환한다 */
			public int GetHP() {
				return m_nHP;
			}

			/** 이름을 반환한다 */
			public string GetName() {
				return m_oName;
			}

			/** 레벨을 변경한다 */
			public void SetLV(int a_nLV) {
				m_nLV = a_nLV;
			}

			/** 체력을 변경한다 */
			public void SetHP(int a_nHP) {
				m_nHP = a_nHP;
			}

			/** 식별자를 변경한다 */
			public void SetID(string a_oID) {
				ID = a_oID;
			}

			/** 이름을 변경한다 */
			public void SetName(string a_oName) {
				m_oName = a_oName;
			}

			/*
			 * 맴버 메서드는 맴버 변수에 접근 할 수 있는 특징이 존재하기 때문에 특정 맴버 메서드가 맴버 변수를 필요로
			 * 할 경우 해당 정보를 입력으로 전달하지 않아도 된다는 것을 알 수 있다. (즉, 클래스는 변수와 메서드의 집합이기
			 * 때문에 해당 구문 동작한다는 것을 알 수 있다.)
			 */
			/** 정보를 출력한다 */
			public void ShowInfo() {
				Console.WriteLine("=====> {0} 정보 <=====", m_oName);
				Console.WriteLine("ID : {0}, LV : {1}, HP : {2}", ID, m_nLV, m_nHP);
			}
		}
#elif E08_CLASS_02

#elif E08_CLASS_03

#endif // E08_CLASS_01

		/** 초기화 */
		public static void Start(string[] args) {
#if E08_CLASS_01
			/*
			 * 클래스는 사용자 정의 자료형이기 때문에 특정 클래스를 사용해서 변수를 선언하는 것이 가능하다. 이때, 특정
			 * 클래스를 통해 선언 된 변수는 객체라고 지칭되기 때문에 클래스를 통한 변수의 선언은 변수를 선언한다는 
			 * 표현보다 객체를 생성한다는 표현을 사용하는 것이 일반적인 관례이다.
			 * 
			 * 즉, 클래스는 객체를 생성하기 위한 틀의 개념이라는 것을 알 수 있다. 또한, 클래스는 사물의 특징을 표현하지만
			 * 구체적인 정보는 빠져 있으며 해당 정보는 객체의 생성을 통해 설정하는 것이 가능하다.
			 * 
			 * 객체의 특정 맴버에 접근하기 위해서는 . (맴버 지정 연산자) 를 사용하면 된다. (즉, 객체는 변수와 메서드를
			 * 포함하고 있기 때문에 특정 객체 하위에 존재하는 특정 맴버에 접근하기 위해서는 반드시 맴버 지정 연산자를
			 * 사용해야한다.)
			 */
			CPCharacter oCharacterA = new CPCharacter();
			oCharacterA.SetID("1");
			oCharacterA.LV = 1;
			oCharacterA.HP = 20;
			oCharacterA.Name = "캐릭터 A";

			CPCharacter oCharacterB = new CPCharacter(20, 850);
			oCharacterB.SetID("2");

			CPCharacter oCharacterC = new CPCharacter(40, 1500, "캐릭터 C");
			oCharacterC.SetID("3");

			Console.WriteLine("=====> {0} 정보 <=====", oCharacterA.Name);
			Console.WriteLine("ID : {0}, LV : {1}, HP : {2}\n", oCharacterA.ID, oCharacterA.LV, oCharacterA.HP);

			oCharacterB.ShowInfo();

			Console.WriteLine();
			oCharacterC.ShowInfo();

#elif E08_CLASS_02

#elif E08_CLASS_03

#endif // E08_CLASS_01
		}
	}
}
