# linage-cm01-launcher
cm01's Linage launcher  
Binary file compiled from this project does not use memory hacking skills, it just runs executable file with parameters.  
In order to compile this file correctly, v2.05.58.g versions of dxwnd.exe, dxwnd.dll, dxwnd.ini and cm01.dll files are required. These files were not uploaded due to copyright and legal issues.  
The dxwnd-related files that need to be in the Resources folder must be bundled into one compressed file, and the cm01.dll file must also be compressed with cm01.zip.  
  
  
이 프로젝트를 빌드한 결과물의 바이너리 파일만으로는 타겟 프로세스의 메모리를 해킹하지 않으며, exe 파일에 인자를 덧붙여 실행만 해줍니다.  
프로젝트를 제대로 컴파일 하기 위해서는 v2.05.58.g 버전의 dxwnd.exe와 dxwnd.dll, dxwnd.ini 파일이 필요합니다. 또한 이 3개의 파일은 확장자 없이 WindowMode 라는 이름으로 압축된 상태로 Resources 폴더에 위치해야 합니다.  
cm01.dll 파일 또한 cm01.zip 이름으로 압축되어서 Resources 폴더에 위치해야 정상적으로 컴파일 할 수 있습니다.  
위에서 언급한 파일들은 저작권 문제 이슈로 인해 함께 업로드 할 수 없습니다. -> Attribution-NonCommercial-ShareAlike 4.0 International (CC BY-NC-SA 4.0)  
  
  
# 기
1. 종료 시 켜져있는 모든 타겟 게임이 함께 종료됨 (cm01.dll 사용)  
2. 서버에서 Bad process로 정한 프로세스들이 PC에 존재한다면 자동 종료 (PaymentServer 연동 필요)   
3. 전체 파일 자동 다운로드 및 압축 해제 (PaymentServer 연동 필요)  
4. Tron 코인을 이용한 자동 결제 시스템 (PaymentServer 연동 필요)  
5. 홈페이지 이동  
6. 창모드  
