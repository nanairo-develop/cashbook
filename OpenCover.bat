rem OpenCover.bat
rem �\�����[�V�������[�g�Ŏ��s���邱��

rem OpenCover �̃C���X�g�[����
SET OPEN_COVER=%USERPROFILE%\.nuget\packages\opencover\4.7.1221\tools

rem Report�����c�[���̃C���X�g�[����
SET REPORT_GEN=%USERPROFILE%\.nuget\packages\reportgenerator\5.1.25\tools\net7.0

rem MSTEST �̃C���X�g�[����
rem set MSTEST="C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe"

rem �e�X�g�t���[�����[�N�̃C���X�g�[����
SET MS_TEST=%ProgramFiles%\Microsoft Visual Studio\2022\Community

rem �^�[�Q�b�g�A�Z���u�� (�e�X�g�N���X������DLL�t�@�C��)
SET TARGET_TEST=TestCashbook.dll

rem �^�[�Q�b�g�A�Z���u���̊i�[�� (�e�X�g�N���X������ꏊ)
SET TARGET_TEST_DIR=C:\Users\user\source\repos\cashbook\TestCashbook\bin\Debug\net7.0-windows

rem OpenCover �̏o�̓t�@�C��
set OUTPUT=%1

rem �J�o���b�W�v���Ώۂ̎w��
rem set FILTERS="+[OpenCoverSample]*"
set FILTERS="+[*]*"

REM �p�X�̐ݒ�
SET PATH=%PATH%;%OPEN_COVER%;%MS_TEST%;%REPORT_GEN%

REM OpenCover�����s (./CoverageReport�t�H���_�֌��ʏo��)
OpenCover.Console -register:user -target:"%MS_TEST%\Common7\IDE\Extensions\TestPlatform\vstest.console.exe" -targetargs:"%TARGET_TEST%" -targetdir:"%TARGET_TEST_DIR%" -filter:"%FILTERS%" -output:".\CoverageReport\result.xml"

REM ReportGenerator�����s (./CoverageReport�t�H���_�֌��ʏo��)
ReportGenerator -reports:".\CoverageReport\result.xml" -reporttypes:Html -targetdir:".\CoverageReport"

pause