# decode19it02
de:code 2019 セッションIT02で利用したサンプルコード

[Azure IoT Hub クラウド側の最新機能：デモも交えてご紹介](https://www.microsoft.com/ja-jp/events/decode/2019session/detail.aspx?sid=IT02&dy=2)

- $web : Storageに配置し、静的Webサイトを構築する（[Azure Storage での静的 Web サイト ホスティング](https://docs.microsoft.com/ja-jp/azure/storage/blobs/storage-blob-static-website)）
- console : IoT Hub へのデバイス一括登録を実施するサンプル。元ネタ:[GitHub:Azure-Samples/azure-iot-samples-csharp](https://github.com/Azure-Samples/azure-iot-samples-csharp)
- functions : IoT Hub へのデバイス一覧取得と１件登録のサンプルAPI。元ネタ:[GitHub:Azure-Samples/azure-iot-samples-csharp](https://github.com/Azure-Samples/azure-iot-samples-csharp)

**VisualStudio 2017で作成しています**


作り方
--------------------
0. 共通
- IoT Hubを１つ作成する
- IoT Hubの共有アクセスポリシーより、iothubownerの共有アクセスキー **接続文字列—プライマリキー** をコピーしておく（A）
1. デバイス一括登録するコンソールアプリ
- Program.csをエディタで開き **HostName=～～.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=～～=** という記述部分を上記(A)で上書きする

2. デバイス一覧取得と１件登録するWebアプリ
- 静的Webサイトホスティング用にAzure Storageを１つ作成する
- Function Appを２つ作成する（１つは一覧検索用:decode19it02sdklist、１つはデバイス登録用:decode19it02sdkregist）
- 各Functions内、Function1.csをエディタで開き **HostName=～～.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=～～=** という記述部分を上記(A)で上書きする(一覧と登録、各々１か所)
- Function App２つをデプロイする
- Function App各々のURLをコピーしておく（decode19it02sdklist:(B)、decode19it02sdkregist:(C)）
- $web内、index.htmlをエディタで開き **https://～～.azurewebsites.net/api/decode19it02sdklist** という記述部分を上記(B)で上書きする
- $web内、index.htmlをエディタで開き **https://～～.azurewebsites.net/api/decode19it02sdkregist** という記述部分を上記(C)で上書きする
- $web内、index.htmlをAzure Storageにホストする
- Azure Storageの静的なWebサイトに記載されている **プライマリエンドポイント** のURLをブラウザで開く

注意
--------------------
これはサンプルです。
実装はできるだけシンプルになるように努めております。
結果として、通常はやらないような実装をしている箇所、本来実装しなければならないのに実装していない箇所があります。
実際の運用に際しては、適切なアクセス権限付与や接続文字列の隠蔽、エラー処理等を実施ください。
