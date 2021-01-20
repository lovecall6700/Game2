# Game2

ストーリー
=====
南太平洋のとある場所に島が一夜にして出現した。その島に上陸したものの多くは帰ってこなかった。未知の生物が跋扈する危険な島だったのだ。ある者が島で宝箱を見たと話したことで行方不明者が急増した。しかし・・・数年たっても宝を持ち帰ったものはいなかった。そして、今日、また一人の命知らずが島に上陸した。あなたは若き冒険家シルヴィアとなって島から宝を持ち帰らなければならない。謎の島の奥を目指せ！隠された扉を見つけろ！宝箱を集めて得点を稼げ！今、レトロゲームの悪意が牙をむく！


操作
=====
キャラクターを動かすだけでなく、メニュー画面での操作も存在します。

キーボード
-----

- `左矢印` または `A` : 左に移動
- `右矢印` または `D` : 右に移動
- `スペース` : ジャンプ
- `B` : 銃を撃つ、決定
- `上矢印` または `W` : 扉に入る、ハシゴを上に移動、メニューのカーソルを上へ移動
- `下矢印` または `S` : しゃがむ、ハシゴを下に移動、隠されたものを探す、メニューのカーソルを下へ移動
- `P` : 一時停止 または 一時停止解除
- `Alt`を押しながら`F4` : 終了
- `ESC` : タイトル画面に戻る
- `Alt`を押しながら`Enter` : フルスクリーン表示に切り替える または ウィンドウ表示に戻す
- `F12` : スクリーンショットを保存

ゲームパッド
-----

- `左` : 左に移動
- `右` : 右に移動
- `A` : ジャンプ
- `B` : 銃を撃つ、決定
- `上` : 扉に入る、ハシゴを上に移動、メニューのカーソルを上へ移動
- `下` : しゃがむ、ハシゴを下に移動、隠されたものを探す、メニューのカーソルを下へ移動
- `Back` : 一時停止 または 一時停止解除


残機
=====
ゲーム画面左上のREMAINが残機です。残機の数だけミスをしてもゲームを続けることができます。残機がゼロになるとゲームオーバーです。得点を稼いで増やすことができます。


ライフ
=====
ゲーム画面左上のREMAINの下にあるLIFEが敵に当たってもいい回数です。最大値は3です。ゼロになるとミスになります。宝箱やアイテムをとることで最大値まで回復できます。


制限時間
=====
画面上中央のカウントがゼロになるとミスになります。ゼロになる前に扉に入りましょう。


得点
=====
ゲーム画面右上に表示されます。ハイスコアは保存されます。一定の得点を獲得するごとに残機が増加します。


各種セーブデータ・設定ファイルパス
=====
下記フォルダ内に各種セーブデータが保管されます。このフォルダ内に`KeyConfig.txt`ファイルをコピーすることでキー操作を変更できます。`AboutKeyConfig.txt`に書かれているキー名を参考に設定を変えてみてください。

- `Windows` : `C:\Users\(ユーザー名)\AppData\Roaming\SHIRAISHI\Game2\1.0.0.0`
- `Linux` : `/home/(ユーザー名)/.Config/SHIRAISHI/Game2/1.0.0.0`
- `macOS` : `Game2.dll`と同じフォルダ内の`SHIRAISHI/Game2/1.0.0.0`


扉
=====
扉に触れて上を押すことで別のステージに移動できます。隠された扉も存在します。隠された扉はしゃがむことで発見することができます。隠された扉を発見することで得点も獲得できます。隠された扉以外に見えない扉も存在し、触れるだけで別のステージに移動します。ミスでキャラクターが落下し画面外に出てしまう前に扉に触れるとミスにはならず、ライフが1の状態で次のステージへ逃げ込むことができます。


アイテム
=====
絵の描かれた四角いプレートはアイテムです。取得することで様々な効果が得られます。同時にライフも回復します。アイテムごとに一定の条件で効果が失われます。アイテムは全9種類です。プレートの絵を見て効果を考えてみましょう。隠されたアイテムも存在します。隠されたアイテムはしゃがむことで発見し取得することができます。ミスでキャラクターが落下し画面外に出てしまう前にアイテムに触れてもアイテムを獲得できます。一度取得したアイテムは復活しません。隠されたアイテムを発見することで得点も獲得できます。例えば以下のようなものがあります。

- 得点倍増
- 敵から無敵になる
- 時間経過半減
- 暗闇ステージが常に明るくなる
- 敵を一撃で倒す


宝箱
=====
宝箱に触れると宝箱が開き、得点を獲得できます。同時にライフも回復します。ミスでキャラクターが落下し画面外に出てしまう前に宝箱に触れても得点を獲得できます。一度開いた宝箱は復活しません。


敵
=====
銃を撃って敵を殺しましょう。ただし、敵を殺しても通りやすくなるだけで、得られるものはありません。唯一、ボスを殺すことでエンディングを見ることができます。


その他
=====

- `F12`キーでスクリーンショットを`各種セーブデータ・設定ファイルパス`に保存できます。一瞬、画面がチラつきます。
- タイトル画面で`End`を選ぶとゲームが終了します。最初から遊ぶ場合は`Start`、`Continue`を選択するとゲームオーバー画面で`Save`した状態から再開します。
- タイトル画面をしばらく見ているとストーリーが表示されます。ストーリーの表示中に`B`ボタンを押すとタイトル画面に戻ります。
- ゲームオーバー画面で`Retry`を選ぶと、すぐに同じステージに再挑戦できます。`Save`で続きから遊べるように状態を保存します。`Retry`でも`Continue`でも一度取ったアイテムや宝箱は復活しません。`End`でタイトル画面に戻ります。
- 扉に入った後に表示されるステージ開始画面で所有しているアイテムとハイスコアを確認することができます。
- 通り抜けることができる壁や柱も存在します。
- 各画面に切り替わって少しの間、入力を受け付けない時間が存在します。不具合ではありません。
- 全16ステージを順番に攻略する必要はありません。ステージ番号の順番に移動することはできません。
- ゲームクリア後、エンディングの最後に獲得した得点とクリアタイムが表示されます。ハイスコアを更新した場合は赤文字で表示されます。クリアタイムはタイトル画面で`Start`を選択すると同時に計測開始、エンディング画面が始まった瞬間に計測終了です。タイトル画面から`Continue`で開始した場合と隠しコマンドを使った場合はクリアしても計測結果は表示されません。
- タイトル画面の`Options`を選ぶとオプション画面に移動します。`BGM`と`SE`の音量をそれぞれ変更可能です。変更した音量は保存され、次回起動時にも適用されます。
- フォントはPixelMplusを利用しています。ソースからコンパイルする場合はダウンロードしたファイルを展開し、ttfファイルを右クリックして`すべてのユーザーに対してインストール`を選んでください。
- このゲームはSHIRAISHIによって緊急事態宣言の間に製造が開始されました。


実行環境
=====
[Game2 Releases](https://github.com/lovecall6700/Game2/releases "Game2 Releases")からダウンロードできます。それぞれの実行環境に合わせてzipファイルをダウンロードしてください。v2系とv3系の2種類があります。ダウンロードするファイルが表示されていない場合は、すぐ下の「Assets」を左クリックすると表示されます。

実行した結果パソコンが爆発したり、死人が出ても俺は知らん。

説明書は`Documents`フォルダ内の`Manual.txt`です。


32bit版/64bit版両方のWindows 7/10、32bit版/64bit版両方のLinux、64bit版のmacOS
-----
v3系の「Game2_(バージョン番号).zip」をダウンロードしてください。

[.NET Core Runtime 3.1.10](https://dotnet.microsoft.com/download/dotnet-core/3.1 ".NET Core Runtime 3.1.10")のインストールが必要です。3.1.11や3.1.12など新しいバージョンがあれば、そちらを使ったほうがいいかもしれません。

`Windows`ではダウンロードしたzipファイルを右クリックし`プロパティ`を選び、`ブロックの解除`にチェックを入れるか`ブロックの解除`ボタンを押し、`OK`ボタン押してください。その後、zipファイルを再び右クリックして`すべて展開`から`展開`を押してください。

`Linux`と`macOS`ではシェルから`dotnet Game2.dll`でゲームが開始します。
`Windows 7/10`では`Game2.exe`をダブルクリックするとゲームが開始します。


32bit版の`Linux`
-----
v3系の「Game2_(バージョン番号)_dotnet452.zip」をダウンロードしてください。

[Mono](https://www.mono-project.com/download/stable/#download-lin "Mono")の`mono-runtime`パッケージのインストールが必要です。

シェルから`mono Game2.exe`でゲームが開始します。


`Windows Vista`
-----
v2系の「Game2_(バージョン番号).zip」をダウンロードしてください。`Windows Vista`ではBGMが演奏されません。

`Windows`ではダウンロードしたzipファイルを右クリックし`プロパティ`を選び、`ブロックの解除`にチェックを入れるか`ブロックの解除`ボタンを押し、`OK`ボタン押してください。その後、zipファイルを再び右クリックして`すべて展開`から`展開`を押してください。

`Game2.exe`をダブルクリックするとゲームが開始します。


更新内容
=====
v2.17およびv3_test21では敵デザインを長らく先送りしていた正式版に差し替えました。


ライセンス
=====
常識の範囲でお使いください。
コレを使って何か面白いことをするなら教えてください。
