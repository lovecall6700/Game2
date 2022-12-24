# Game2

## ストーリー

南太平洋のとある場所に島が一夜にして出現した。その島に上陸した者の多くは帰ってこなかった。未知の生物が跋扈する危険な島だったのだ。ある者が島で宝箱を見たと話したことで行方不明者が急増した。しかし・・・数年たっても宝を持ち帰った者はいなかった。そして、今日、また一人の命知らずが島に上陸した。あなたは若き冒険家シルヴィアとなって島から宝を持ち帰らなければならない。謎の島の奥を目指せ！隠された扉を見つけろ！宝箱を集めて得点を稼げ！今、レトロゲームの悪意が牙をむく！


## 操作方法

キャラクターの操作とメニュー画面の操作が存在します。


### パソコンキーボード


- `左` または `A` : 左に移動
- `右` または `D` : 右に移動
- `スペース` : ジャンプ
- `B` : 銃を撃つ、決定(メニュー画面)
- `上` または `W` : 扉に入る、ハシゴを上に移動、カーソルを上へ移動(メニュー画面)
- `下` または `S` : しゃがむ、ハシゴを下に移動、隠されたものを発見・取得、カーソルを下へ移動(メニュー画面)
- `P` : 一時停止 または 一時停止解除
- `Alt`を押しながら`F4` : 終了
- `ESC` : 確認なしでタイトル画面に戻る
- `Alt`を押しながら`Enter` : フルスクリーン表示とウィンドウ表示を切り替える
- `F12` : スクリーンショットを保存


### ゲームパッド


- `左` : 左に移動
- `右` : 右に移動
- `A` : ジャンプ
- `B` : 銃を撃つ、決定
- `上` : 扉に入る、ハシゴを上に移動、メニューのカーソルを上へ移動
- `下` : しゃがむ、ハシゴを下に移動、隠されたものを発見・取得、メニューのカーソルを下へ移動
- `Back` : 一時停止 または 一時停止解除


## 画面


### タイトル画面

`Start`を選択すると、ゲームを最初から遊ぶことができます。
`Continue`を選択すると、ゲームオーバー画面で`Save`した続きから遊ぶことができます。
`Options`を選択すると、オプション画面へ移動します。
`End`を選択すると、ゲームが終了します。
しばらく操作せずにいるとストーリーが表示されます。ストーリーの表示中に`B`ボタンを押すとタイトル画面に戻ります。


### オプション画面

`BGM`と`SE`を選択すると、それぞれの音量を変更できます。音量の変更が完了したら、`End`を選択することでオプション画面に戻ります。変更した音量は保存され、次回起動時にも適用されます。
`End`を選択すると、タイトル画面に戻ります。


### ステージ開始画面

ゲームスタート直後と扉に入った後、ステージ開始画面が表示されます。移動先のステージ番号、所有しているアイテムとハイスコアを確認することができます。Bボタンでスキップできます。
ステージ開始画面の表示時間もクリアタイムに含まれます。


### ゲーム画面


#### 残機

ゲーム画面左上の`REMAIN`は残機です。ミスすると減少し、ゼロになるとゲームオーバーです。得点を多く獲得すれば増やすことができます。


#### 体調

ゲーム画面左上の`LIFE`は主人公の体調です。敵に接触すると減少し、ゼロになるとミスになります。敵によって減少する値は異なります。最大値は3です。宝箱やアイテムをとることで最大値まで回復します。


#### 制限時間

ゲーム画面上部中央の数値は制限時間です。時間とともに減少し、ゼロになるとミスになります。ゼロになる前に扉に入りましょう。


#### 得点

ゲーム画面右上の数値は得点です。一定の得点を獲得するごとに残機が増加します。ハイスコアは保存されます。


#### 扉

扉に触れて`上`を押すことで別のステージに移動できます。`下`を押してしゃがむことで隠された扉を発見することができます。発見すると得点も獲得できます。見えない扉も存在します。見えない扉に触れると別のステージに移動します。ミスでキャラクターが落下中、画面外に出てしまう前に扉に触れるとミスにはならず、体調が1の状態で次のステージへ逃げ込むことができます。


#### アイテム

絵の描かれた四角いプレートはアイテムです。取得することで様々な効果が得られます。同時に体調も回復します。アイテムごとに一定の条件で効果が失われます。アイテムは全9種類です。プレートの絵を見て効果を考えてみましょう。`下`を押してしゃがむことで隠されたアイテムを発見・取得することができます。発見すると得点も獲得できます。ミスでキャラクターが落下中、画面外に出てしまう前にアイテムに触れるとアイテムを獲得できます。一度取得したアイテムは復活しません。アイテムには以下のようなものがあります。

- 得点倍増
- 敵から無敵になる
- 時間経過半減
- 暗闇ステージが常に明るくなる
- 敵を一撃で倒す


#### 宝箱

宝箱に触れると得点を獲得できます。同時に体調も回復します。`下`を押してしゃがむことで隠された宝箱を発見・取得することができます。ミスでキャラクターが落下中、画面外に出てしまう前に宝箱に触れると得点を獲得できます。一度開いた宝箱は復活しません。


#### 敵

銃を撃って敵を殺しましょう。ただし、敵を殺しても得られるものはありません。


### ゲームオーバー画面

`Retry`を選ぶと、すぐに同じステージに再挑戦できます。
`Save`を選ぶと、状態を保存できます。ゲームを終了しても、タイトル画面から`Continue`で続きを遊ぶことができます。ゲームオーバーにならなければ`Save`できません。`Retry`や`Continue`では既に獲得したアイテムや宝箱は復活しません。得点もゼロになります。
`End`でタイトル画面に戻ります。
ゲームオーバー画面の表示時間もクリアタイムに含まれます。


### エンディング画面

氷に閉ざされた谷底に潜むボスを殺すとエンディングを見ることができます。
エンディング画面の最後に獲得した得点とクリアタイムが表示されます。ハイスコアを更新した場合は赤文字で表示されます。クリアタイムはタイトル画面で`Start`を選択すると同時に計測開始、エンディング画面が始まった瞬間に計測終了です。タイトル画面から`Continue`で開始した場合や隠しコマンドを使った場合、クリアタイムは表示されません。
Finが表示されたら、Bボタンを押してタイトル画面に戻れます。


## 各種セーブデータ・設定ファイルパス

下記フォルダ内に各種セーブデータが保管されます。このフォルダ内に`KeyConfig.txt`ファイルをコピーすることでキー操作を変更できます。`AboutKeyConfig.txt`に書かれているキー名を参考に設定を変えてみてください。

- `Windows` : `C:\Users\(ユーザー名)\AppData\Roaming\SHIRAISHI\Game2\1.0.0.0`
- `Linux` : `/home/(ユーザー名)/.Config/SHIRAISHI/Game2/1.0.0.0`
- `macOS` : `Game2.dll`と同じフォルダ内の`SHIRAISHI/Game2/1.0.0.0`


## その他


- タイトル画面・オプション画面・ゲームオーバー画面において、各種ボタンを長押しや連打していると、ボタンに反応しません。一度ボタンから手を放し、押しなおしてください。
- `F12`キーでスクリーンショットを撮影できます。`各種セーブデータ・設定ファイルパス`に保存されます。一瞬、画面がチラつきます。
- vs UFOルートに行きたい人はスタート地点から右側に大きくジャンプして水中に飛び込みましょう。
- このゲームはSHIRAISHIによって第1回緊急事態宣言(2020年4月7日～5月25日)の期間に製造され、2022年12月31日にメンテナンスを終了しました。


## 実行環境

Game2は`Game2 Releases`( https://github.com/lovecall6700/Game2/releases )からダウンロードできます。実行環境ごとに用意されたzipファイルをダウンロードしてください。ダウンロードするファイルが表示されていない場合は、「Assets」をクリックすると表示されます。

ランタイムのインストールが難しくてわからない方は「Game2_v2.(更新番号).zip」をダウンロードしてください。Vista/7/8/10、どのWindowsでも動作すると思います。

実行してパソコンが爆発したり、死人が出ても俺は知らん。

説明書は`Documents`フォルダ内の`Manual.txt`です。`README.mf`と`Manual.txt`は同じものです。


### Windows 7/8/10 (32bit/64bit)、Linux (64bit)、macOS (64bit)

「Game2_v3.(更新番号).zip」をダウンロードしてください。

`.NET Runtime 3.1`( https://dotnet.microsoft.com/download/dotnet-core/3.1 )のインストールが必要です。.NET Core 3.1は2022年12月31日にマイクロソフトによるメンテナンスが終了しました。.NET 5以降のバージョンでGame2が動作するかは知りません。

`Windows`ではダウンロードしたzipファイルを右クリックし、`プロパティ`を選び、`ブロックの解除`にチェックを入れるか、`ブロックの解除`ボタンを押し、`OK`ボタンを押してください。その後、再びzipファイルを右クリックし、`すべて展開`を選択し、`展開`ボタンを押してください。zipファイルが展開されます。

`Linux`と`macOS`では、シェルから展開したフォルダ内に移動し、`dotnet Game2.dll`でゲームが開始します。
`Windows 7/8/10`では、展開したフォルダ内の`Game2.exe`をダブルクリックするとゲームが開始します。


### Linux (32bit)

「Game2_v3.(バージョン番号)_net452.zip」をダウンロードしてください。

`Mono`( https://www.mono-project.com/download/stable/#download-lin )の`mono-runtime`パッケージのインストールが必要です。

シェルから展開したフォルダ内に移動し、`mono Game2.exe`でゲームが開始します。


### Windows汎用 Vista/7/8/10

「Game2_v2.(更新番号).zip」をダウンロードしてください。`Windows Vista`ではBGMが演奏されません。

`Windows`ではダウンロードしたzipファイルを右クリックし、`プロパティ`を選び、`ブロックの解除`にチェックを入れるか、`ブロックの解除`ボタンを押し、`OK`ボタンを押してください。その後、再びzipファイルを右クリックし、`すべて展開`を選択し、`展開`ボタンを押してください。zipファイルが展開されます。

展開したフォルダ内の`Game2.exe`をダブルクリックするとゲームが開始します。


## ソースコードからビルドする

- Windows以外のビルド環境は不明です。ビルドしたバイナリは、他の環境でも適切なランタイムをインストールすれば動作します。
- `PixelMplus10 Regular` ( https://itouhiro.hatenablog.com/entry/20130602/font )をインストールしてください。ダウンロードしたファイルを展開し、`PixelMplus10-Regular.ttf`を右クリックして`すべてのユーザーに対してインストール`を選んでください。
- `VisualStudio 2019`をインストールしてください。私は`.NETデスクトップ開発`、`.NETによるモバイル開発`、`.NETクロスプラットフォーム開発`を選択しました。
- `MonoGame 3.8.0` ( https://docs.monogame.net/articles/getting_started/0_getting_started.html ) をインストールしてください。3.8.1以降に置き換わってしまっているかもしれませんが、自力でなんとかしてください。オフラインインストーラを公開している人がいます。利用できるかもしれません。
- 古いWindowsや32bit Linuxで環境でバイナリを動作させたい場合は`VisualStudio 2017`と`MonoGame 3.7.1`をインストールします。`VisualStudio 2017`は`MonoGame 3.7.1`のインストール時に参照されるだけです。最小限のインストールで構いません。私はアンインストールしてしまいました。`Documents\Visual Studio 2017\Templates\ProjectTemplates\Visual C#`フォルダ内の`MonoGame`フォルダを`Documents\Visual Studio 2019\Templates\ProjectTemplates\Visual C#`フォルダ内にコピーすると`VisualStudio 2019`から`MonoGame 3.7.1`用のプロジェクトを作成することもできます。
- Game2のソースコードをクローンするかダウンロードしてください。
- ビルドしたいブランチを選んでください。`.NET Runtime 3.1`環境で動作させたいならブランチ`monogame3.8gl`を、古いWindowsや32bit Linuxで環境で動作させたいならブランチ`monogame3.7dx`を、Android環境なら`monogame3.8android`を選びます。Android環境は起動するだけでタッチパネルでは遊べません。ゲームパッドをAndroid端末に接続すれば遊べるのかもしれませんが、試したことはありません。
- `Game2.sln`をダブルクリックしてください。開発環境が起動します。


## ライセンス

常識の範囲でお使いください。
コレを使って何か面白いことをするなら教えてください。
