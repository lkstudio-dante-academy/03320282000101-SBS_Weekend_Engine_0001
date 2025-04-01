#define EXAMPLE
#define PRACTICE
#define TRAINING

namespace Example
{
	internal class Program
	{
		/*
		 * 메인 메서드란?
		 * - C# 으로 제작 된 프로그램이 실행 될 때 가장 먼저 호출 (실행) 되는 메서드를
		 * 의미한다. (즉, 메인 메서드가 실행 되었다는 것은 프로그램이 구동 되었다는
		 * 것을 의미한다.)
		 * 
		 * 또한, 메인 메서드가 종료되면 프로그램도 종료되는 특징이 존재한다. 따라서,
		 * C# 으로 프로그램을 제작 할 때는 반드시 메인 메서드를 구현해야한다.
		 * 
		 * 만약, 메인 메서드를 구현하지 않았을 경우 작성 된 프로그램에서 어떤 부분을
		 * 먼저 실행해야하는지 구분 하는 것이 불가능하기 때문에 프로그램 제작 자체가
		 * 안된다.
		 * 
		 * 주석이란?
		 * - 컴퓨터가 아닌 사용자 (프로그래머) 를 위한 기능으로 메모를 작성 할 수 있는
		 * 기능을 의미한다. (즉, 주석을 활용하면 특정 명령어를 이해하는데 필요한 개념들을
		 * 정리하는 것이 가능하다.)
		 * 
		 * 메서드란?
		 * - 정해진 특정 역할을 수행하는 기능을 의미한다. (즉, 메서드를 호출하면 해당 메서드에
		 * 존재하는 명령어들이 실행된다는 것을 의미한다.)
		 */
		static void Main(string[] args)
		{
#if EXAMPLE
			/*
			 * Visual Studio 주석 관련 단축키
			 * - Ctrl + K, C (주석 처리)
			 * - Ctrl + K, U (주석 해제)
			 */
			//_03020203000101_SBS_Weekend_Engine_0001.E01.Example.Classes.Example_01.CE01Example_01.Start(args);
			//_03020203000101_SBS_Weekend_Engine_0001.E01.Example.Classes.Example_02.CE01Example_02.Start(args);
			//_03020203000101_SBS_Weekend_Engine_0001.E01.Example.Classes.Example_03.CE01Example_03.Start(args);
			//_03020203000101_SBS_Weekend_Engine_0001.E01.Example.Classes.Example_04.CE01Example_04.Start(args);
			//_03020203000101_SBS_Weekend_Engine_0001.E01.Example.Classes.Example_05.CE01Example_05.Start(args);
			//_03020203000101_SBS_Weekend_Engine_0001.E01.Example.Classes.Example_06.CE01Example_06.Start(args);
			//_03020203000101_SBS_Weekend_Engine_0001.E01.Example.Classes.Example_07.CE01Example_07.Start(args);
			//_03020203000101_SBS_Weekend_Engine_0001.E01.Example.Classes.Example_08.CE01Example_08.Start(args);
			//_03020203000101_SBS_Weekend_Engine_0001.E01.Example.Classes.Example_09.CE01Example_09.Start(args);
			//_03020203000101_SBS_Weekend_Engine_0001.E01.Example.Classes.Example_10.CE01Example_10.Start(args);
			_03020203000101_SBS_Weekend_Engine_0001.E01.Example.Classes.Example_11.CE01Example_11.Start(args);
#elif PRACTICE
			//_03020203000101_SBS_Weekend_Engine_0001.E01.Practice.Classes.Practice_01.CP01Practice_01.Start(args);
			//_03020203000101_SBS_Weekend_Engine_0001.E01.Practice.Classes.Practice_02.CP01Practice_02.Start(args);
			//_03020203000101_SBS_Weekend_Engine_0001.E01.Practice.Classes.Practice_03.CP01Practice_03.Start(args);
			//_03020203000101_SBS_Weekend_Engine_0001.E01.Practice.Classes.Practice_04.CP01Practice_04.Start(args);
			_03020203000101_SBS_Weekend_Engine_0001.E01.Practice.Classes.Practice_05.CP01Practice_05.Start(args);
#elif TRAINING
			//_03020203000101_SBS_Weekend_Engine_0001.E01.Training.Classes.Training_01.CT01Training_01.Start(args);
			//_03020203000101_SBS_Weekend_Engine_0001.E01.Training.Classes.Training_02.CT01Training_02.Start(args);
			_03020203000101_SBS_Weekend_Engine_0001.E01.Training.Classes.Training_03.CT01Training_03.Start(args);
#endif // #if EXAMPLE

			System.Console.ReadKey();
		}
	}
}
