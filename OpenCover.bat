rem OpenCover.bat
rem ソリューションルートで実行すること

rem OpenCover のインストール先
SET OPEN_COVER=%USERPROFILE%\.nuget\packages\opencover\4.7.1221\tools

rem Report生成ツールのインストール先
SET REPORT_GEN=%USERPROFILE%\.nuget\packages\reportgenerator\5.1.25\tools\net7.0

rem MSTEST のインストール先
rem set MSTEST="C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe"

rem テストフレームワークのインストール先
SET MS_TEST=%ProgramFiles%\Microsoft Visual Studio\2022\Community

rem ターゲットアセンブリ (テストクラスがあるDLLファイル)
SET TARGET_TEST=TestCashbook.dll

rem ターゲットアセンブリの格納先 (テストクラスがある場所)
SET TARGET_TEST_DIR=C:\Users\user\source\repos\cashbook\TestCashbook\bin\Debug\net7.0-windows

rem OpenCover の出力ファイル
set OUTPUT=%1

rem カバレッジ計測対象の指定
rem set FILTERS="+[OpenCoverSample]*"
set FILTERS="+[*]*"

REM パスの設定
SET PATH=%PATH%;%OPEN_COVER%;%MS_TEST%;%REPORT_GEN%

REM OpenCoverを実行 (./CoverageReportフォルダへ結果出力)
OpenCover.Console -register:user -target:"%MS_TEST%\Common7\IDE\Extensions\TestPlatform\vstest.console.exe" -targetargs:"%TARGET_TEST%" -targetdir:"%TARGET_TEST_DIR%" -filter:"%FILTERS%" -output:".\CoverageReport\result.xml"

REM ReportGeneratorを実行 (./CoverageReportフォルダへ結果出力)
ReportGenerator -reports:".\CoverageReport\result.xml" -reporttypes:Html -targetdir:".\CoverageReport"

pause