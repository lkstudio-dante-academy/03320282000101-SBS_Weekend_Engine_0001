//#define E08_COLLECTION_01
//#define E08_COLLECTION_02
//#define E08_COLLECTION_03
#define E08_COLLECTION_04
#define E08_COLLECTION_05

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 컬렉션이란?
 * - 다수의 데이터를 제어 및 관리 할 수 있는 기능을 의미한다. (즉, 컬렉션을 활용하면 다수의 데이터를 좀 더
 * 수월하게 처리하는 것이 가능하다.)
 * 
 * 일반적으로 프로그램이 처리하는 데이터는 단일 데이터가 아닌 복수의 데이터이기 때문에 컬렉션을 활용하는
 * 것은 좀 더 수월하게 목적에 맞는 프로그램을 제작 할 수 있다는 것을 알 수 있다.
 * 
 * C# 컬렉션 종류
 * - 배열
 * - 리스트 (배열/연결)
 * - 스택/큐
 * - 해시 테이블
 * 
 * 배열이란?
 * - 동일한 자료형의 데이터를 N 개 이상 관리 및 제어 할 수 있는 기능을 의미한다. (단, 배열은 관리하기 위한
 * 데이터의 개수가 한번 정해지고 나면 더이상 변경하는 것이 불가능하다.)
 * 
 * C# 배열 선언 방법
 * - 자료형 + [] + 배열 이름
 * 
 * Ex)
 * int[] anVals = new int[5];
 * string[] aoStrs = new string[5];
 * 
 * int[] anCloneVals = new int[5];
 * 
 * for(int i = 0; i < 5; ++i) {
 *     anCloneVals[i] = anVals[i];
 * }
 * 
 * C# 컬렉션은 참조 타입에 해당하는 자료형이기 때문에 특정 컬렉션을 다른 컬렉션에 복사 할 때 얕은 복사 또는
 * 깊은 복사인지를 구분하는 것이 중요하다.
 * 
 * 얕은 복사 vs 깊은 복사
 * - 특정 참조 타입 변수의 참조 값만을 복사하는 것을 얕은 복사라고 하며, 참조 값이 아닌 참조하고 있는 대상
 * 자체를 복사하는 것을 깊은 복사라고 한다. (즉, 얕은 복사는 복사가 완료되고 나면 동일한 대상을 참조하고 있는
 * 반면, 깊은 복사는 참조하는 대상 자체를 복사하기 때문에 복사가 완료되고 나면 서로 다른 대상을 참조하는
 * 차이점이 존재한다.)
 * 
 * 리스트란?
 * - 관리되는 데이터의 순서가 지정되어있는 컬렉션을 의미한다. (즉, 데이터의 순서가 필요 할 경우 리스트 컬렉션을
 * 활용하면 된다는 것을 의미한다.)
 * 
 * C# 리스트 종류
 * - List (배열)
 * - LinkedList (연결)
 * 
 * 배열 리스트 vs 연결 리스트
 * - 배열 리스트는 배열을 기반으로 데이터의 순서를 보장하기 때문에 배열 기반 리스트는 특정 데이터에 빠르게 접근
 * 할 수 있는 장점이 존재한다. 반면, 특정 위치의 데이터를 추가하거나 제거했을 경우에는 데이터의 순서를 보장하기
 * 위해서 다른 데이터의 이동이 발생하는 단점 존재한다. (즉, 데이터의 삽입/삭제 시 프로그램 성능 저하가 발생 할
 * 수 있다.)
 * 
 * 연결 리스트는 배열 리스트와 달리 특정 데이터에 접근하는 속도가 떨어지는 반면, 새로운 데이터의 삽입 또는 삭제 시
 * 다른 데이터의 이동이 발생하지 않는다. (즉, 데이터의 삽입/삭제가 배열 리스트보다 빠르게 동작한다는 것을 의미
 * 한다.)
 * 
 * 스택이란?
 * - LIFO (Last In First Out) 의 순서를 지니는 컬렉션을 의미한다.
 * 
 * 큐란?
 * - FIFO (First In First Out) 의 순서를 지니는 컬렉션을 의미한디.
 * 
 * 스택/큐 컬렉션은 다른 컬렉션과 달리 특정 데이터에 접근하는 것이 불가능하며, 데이터의 입력과 출력 순서가 강제된다는
 * 특징이 존재한다.
 */
namespace Example.Classes.Example_07 {
	internal class CExample_07 {
		/** 초기화 */
		public static void Start(string[] args) {
#if E08_COLLECTION_01
			int[] anVals01 = new int[5] {
				1, 2, 3, 4, 5
			};

			int[] anVals02 = new int[] {
				1, 2, 3
			};

			int[] anVals03 = null;

			anVals03 = anVals02;
			anVals03[0] = 10;

			int[] anVals04 = (int[])anVals01.Clone();
			anVals04[0] = 10;

			Console.WriteLine("=====> 배열 요소 - 1 <=====");

			/*
			 * Length 프로퍼티는 배열 요소의 총 개수를 가져오는 역할을 수행한다. (즉, 해당 프로퍼티를 활용하면
			 * 1 차원 배열의 길이를 가져오는 것이 가능하다.)
			 */
			for(int i = 0; i < anVals01.Length; ++i) {
				Console.Write("{0}, ", anVals01[i]);
			}

			Console.WriteLine();

			for(int i = 0; i < anVals02.Length; ++i) {
				Console.Write("{0}, ", anVals02[i]);
			}

			Console.WriteLine();

			for(int i = 0; i < anVals03.Length; ++i) {
				Console.Write("{0}, ", anVals03[i]);
			}

			Console.WriteLine();

			for(int i = 0; i < anVals04.Length; ++i) {
				Console.Write("{0}, ", anVals04[i]);
			}

			Console.Write("\n\n개수 입력 : ");
			int nNumVals = int.Parse(Console.ReadLine());

			int[] anVals = new int[nNumVals];

			for(int i = 0; i < anVals.Length; ++i) {
				Console.Write("정수 {0} 입력 : ", i + 1);
				anVals[i] = int.Parse(Console.ReadLine());
			}

			Console.WriteLine("\n=====> 배열 요소 - 2 <=====");

			for(int i = 0; i < anVals.Length; ++i) {
				Console.Write("{0}, ", anVals[i]);
			}

			Console.WriteLine();
#elif E08_COLLECTION_02
			int[,] anVals01 = new int[3, 3] {
				{ 1, 2, 3 },
				{ 4, 5, 6 },
				{ 7, 8, 9 }
			};

			int[,,] anVals02 = new int[2, 2, 2] {
				{
					{ 1, 2 },
					{ 3, 4 }
				},

				{
					{ 5, 6 },
					{ 7, 8 }
				},
			};

			Console.WriteLine("=====> 2 차원 배열 <=====");

			/*
			 * 1 차원 인덱스 -> 2 차원 인덱스로 변환 방법
			 * - 행 : 1 차원 인덱스 / 열 개수
			 * - 열 : 1 차원 인덱스 % 열 개수
			 * 
			 * 2 차원 인덱스 -> 1 차원 인덱스로 변환 방법
			 * - 1 차원 인덱스 : (행 * 열 개수) + 열
			 */
			for(int i = 0; i < anVals01.Length; ++i) {
				int nRow = i / anVals01.GetLength(1);
				int nCol = i % anVals01.GetLength(1);

				Console.Write("{0}, ", anVals01[nRow, nCol]);
			}

			Console.WriteLine("\n");

			/*
			 * GetLength 메서드는 배열의 특정 차원의 길이를 가져오는 역할을 수행한다. (즉, Length 프로퍼티는
			 * 해당 배열에 존재하는 요소의 총 개수를 가져오기 때문에 2 차원 이상의 배열에서는 GetLength 활용
			 * 하는 것이 일반적이다.
			 * 
			 * 해당 메서드에 지정하는 번호는 가장 상위 차원이 0 이고 이후 차원부터는 1 씩 증가 된 수를 명시하면
			 * 된다.
			 */
			for(int i = 0; i < anVals01.GetLength(0); ++i) {
				for(int j = 0; j < anVals01.GetLength(1); ++j) {
					Console.Write("{0}, ", anVals01[i, j]);
				}

				Console.WriteLine();
			}

			Console.WriteLine("\n=====> 3 차원 배열 <=====");

			for(int i = 0; i < anVals02.GetLength(0); ++i) {
				for(int j = 0; j < anVals02.GetLength(1); ++j) {
					for(int k = 0; k < anVals02.GetLength(2); ++k) {
						Console.Write("{0}, ", anVals02[i, j, k]);
					}

					Console.WriteLine();
				}

				Console.WriteLine();
			}
#elif E08_COLLECTION_03
			List<int> oValList01 = new List<int>();
			LinkedList<int> oValList02 = new LinkedList<int>();

			for(int i = 0; i < 10; ++i) {
				oValList01.Add(i + 1);
				oValList02.AddLast(i + 1);
			}

			Console.WriteLine("=====> 배열 리스트 <=====");

			for(int i = 0; i < oValList01.Count; ++i) {
				Console.Write("{0}, ", oValList01[i]);
			}

			Console.WriteLine("\n\n=====> 연결 리스트 <=====");

			/*
			 * foreach 반복문은 일반적으로 컬렉션을 대상으로 해당 컬렉션의 모든 데이터를
			 * 순차적으로 탐색 할 때 사용 된다.
			 * 
			 * 연결 리스트는 랜덤 엑세스가 불가능하기 때문에 리스트의 특정 데이터에 접근하기
			 * 위해서는 항상 처음부터 차례대로 탐색을 수행해야한다.
			 * 
			 * 따라서, foreach 반복문을 활용하면 연결 리스트의 모든 데이터에 접근하는 것이
			 * 가능하다는 것을 알 수 있다.
			 */
			foreach(int nVal in oValList02) {
				Console.Write("{0}, ", nVal);
			}

			oValList01.Insert(3, 100);

			/*
			 * 연결 리스트는 렌덤 엑세스가 불가능하기 때문에 특정 위치에 데이터를 추가하기 위해서는
			 * 해당 위치를 나타내는 노드에 먼저 접근 할 필요가 있다.
			 */
			LinkedListNode<int> oNode = oValList02.Find(3);
			oValList02.AddBefore(oNode, 100);

			Console.WriteLine("\n\n=====> 배열 리스트 - 추가 후 <=====");

			for(int i = 0; i < oValList01.Count; ++i) {
				Console.Write("{0}, ", oValList01[i]);
			}

			Console.WriteLine("\n\n=====> 연결 리스트 - 추가 후 <=====");

			foreach(int nVal in oValList02) {
				Console.Write("{0}, ", nVal);
			}

			oValList01.Remove(100);
			oValList02.Remove(100);

			Console.WriteLine("\n\n=====> 배열 리스트 - 삭제 후 <=====");

			for(int i = 0; i < oValList01.Count; ++i) {
				Console.Write("{0}, ", oValList01[i]);
			}

			Console.WriteLine("\n\n=====> 연결 리스트 - 삭제 후 <=====");

			foreach(int nVal in oValList02) {
				Console.Write("{0}, ", nVal);
			}

			Console.WriteLine();
#elif E08_COLLECTION_04
			Stack<int> oStack = new Stack<int>();
			Queue<int> oQueue = new Queue<int>();

			for(int i = 0; i < 10; ++i) {
				oStack.Push(i + 1);
				oQueue.Enqueue(i + 1);
			}

			Console.WriteLine("=====> 스택 요소 <=====");

			/*
			 * Pop/Dequeue 메서드는 해당 컬렉션으로부터 데이터를 가져오는 역할을 수행한다. 또한, 해당
			 * 메서드가 실행되었을 경우 컬렉션으로부터 데이터가 제거되는 특징이 존재한다.
			 * 
			 * 따라서, 해당 컬렉션에서 데이터를 가져오되 데이터를 제거하는 것을 피하고 싶다면 Peek 메서드를
			 * 사용해야한다.
			 * 
			 * 즉, Peek 와 Pop/Dequeue 메서드 모두 데이터를 가져오는 것이 가능하지만 Peek 는 데이터를 가져오는
			 * 행위만하는 반면, Pop/Dequeue 메서드는 데이터를 제거하는 행위도 같이 수행되는 차이가 존재한다.
			 */
			while(oStack.Count > 0) {
				Console.Write("{0}, ", oStack.Pop());
			}

			Console.WriteLine("\n\n=====> 큐 요소 <=====");

			while(oQueue.Count > 0) {
				Console.Write("{0}, ", oQueue.Dequeue());
			}

			Console.WriteLine();
#elif E08_COLLECTION_05

#endif // #if E08_COLLECTION_01
		}
	}
}
