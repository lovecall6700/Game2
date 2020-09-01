# Game2

ストーリー
=====
南太平洋のとある場所に島が一夜にして出現した。その島に上陸したものの多くは帰ってこなかった。未知の生物が跋扈する危険な島だったのだ。ある者が島で宝箱を見たと話したことで行方不明者が急増した。しかし・・・数年たっても宝を持ち帰ったものはいなかった。そして、今日、また一人の命知らずが島に上陸した。あなたは若き冒険家シルヴィアとなって島から宝を持ち帰らなければならない。謎の島の奥を目指せ！隠された扉を見つけろ！宝箱を集めて得点を稼げ！今、レトロゲームの悪意が牙をむく！


操作
=====
キャラクターを動かすだけでなく、メニュー画面での操作も存在します。

キーボード
-----

- 左矢印 または A：左に移動
- 右矢印 または D：右に移動
- スペース：ジャンプ
- B：銃を撃つ、決定
- 上矢印 または W：扉に入る、ハシゴを上に移動、メニューのカーソルを上へ移動
- 下矢印 または S：しゃがむ、ハシゴを下に移動、隠されたものを探す、メニューのカーソルを下へ移動
- P:一時停止 または 一時停止解除
- ESC または Altを押しながらF4：終了
- Alt+Enter：フルスクリーン表示に切り替える または ウィンドウ表示に戻す
- F12：スクリーンショットを保存

ゲームパッド
-----

- 左：左に移動
- 右：右に移動
- A：ジャンプ
- B：銃を撃つ、決定
- 上：扉に入る、ハシゴを上に移動、メニューのカーソルを上へ移動
- 下：しゃがむ、ハシゴを下に移動、隠されたものを探す、メニューのカーソルを下へ移動
- Back：一時停止 または 一時停止解除


残機
=====
ゲーム画面左上のREAINが残機です。残機の数だけミスをしてもゲームを続けることができます。残機がゼロになるとゲームオーバーです。得点を稼いで増やすことができます。


ライフ
=====
ゲーム画面左上のREAINの下にあるLIFEが敵に当たってもいい回数です。最大値は3です。ゼロになるとミスになります。宝箱やアイテムをとることで最大値まで回復できます。


制限時間
=====
画面上中央のカウントがゼロになるとミスになります。ゼロになる前に扉に入りましょう。


得点
=====
ゲーム画面右上に表示されます。ハイスコアは保存されます。一定の得点を獲得するごとに残機が増加します。


各種セーブデータ・設定ファイルパス
=====
Windowsなら`C:\Users\(ユーザー名)\AppData\Roaming\SHIRAISHI\Game2\1.0.0.0`、Linuxなら`/home/(ユーザー名)/.Config/SHIRAISHI/Game2/1.0.0.0`、macOSはGame2.dllのあるフォルダ内の`SHIRAISHI\Game2\1.0.0.0`に各種セーブデータが保管される。

`KeyConfig.txt`ファイルを用意することでキー操作を好みのキーに変更できます。`KeyConfig.txt`を`SHIRAISHI\Game2\1.0.0.0`内にコピーして、`AboutKeyConfig.txt`に書かれているキー名を参考に変えてみてください。


扉
=====
扉に触れて上を押すことで別のステージに移動できます。隠された扉も存在します。隠された扉はしゃがむことで発見することができます。隠された扉を発見することで得点も獲得できます。隠された扉以外に見えない扉も存在し、触れるだけで別のステージに移動します。ミスでキャラクターが落下し画面外に出てしまう前に扉に触れるとミスにはならず、ライフが1の状態で次のステージへ逃げ込むことができます。


アイテム
=====
絵の描かれた四角いプレートはアイテムです。取得することで様々な効果が得られます。同時にライフも回復します。アイテムごとに一定の条件で効果が失われます。アイテムは全9種類です。プレートの絵を見て効果を考えてみましょう。隠されたアイテムも存在します。隠されたアイテムはしゃがむことで発見し取得することができます。ミスでキャラクターが落下し画面外に出てしまう前にアイテムに触れてもアイテムを獲得できます。一度取得したアイテムは復活しません。隠されたアイテムを発見することで得点も獲得できます。例えば以下のようなものがあります。

- 得点倍増(DoubleScore)
- 敵から無敵になる(Shield)
- 時間経過半減(Time)
- 暗闇ステージが明るくなる(Light)
- 敵を一撃で倒す(Sword)


宝箱
=====
宝箱に触れると宝箱が開き、得点を獲得できます。同時にライフも回復します。ミスでキャラクターが落下し画面外に出てしまう前に宝箱に触れても得点を獲得できます。一度開いた宝箱は復活しません。


敵
=====
銃を撃って敵を殺しましょう。ただし、敵を殺しても通りやすくなるだけで、得られるものはありません。唯一、ボスを殺すことでエンディングを見ることができます。


その他
=====

- F12キーでスクリーンショットを`各種セーブデータ・設定ファイルパス`に保存できます。一瞬、画面がチラつきます。
- タイトル画面でEndを選ぶとゲームが終了します。最初から遊ぶ場合はStart、前回のゲームオーバーから再開する場合はContinueを選択します。
- タイトル画面をしばらく見ているとストーリーが表示されます。ストーリーの表示中にBボタンを押すとタイトル画面に戻ります。
- ゲームオーバー画面でRetryを選ぶと、すぐに同じステージに再挑戦できます。Saveで続きから遊べるように状態を保存します。後日、タイトル画面からContinueを選ぶことで保存した続きを遊ぶことができます。RetryでもContinueでも一度取ったアイテムや宝箱は復活しません。Endでタイトル画面に戻ります。
- 扉に入った後に表示されるステージ開始画面で所有しているアイテムとハイスコアを確認することができます。
- 通り抜けることができる壁や柱も存在します。
- 各画面に切り替わって少しの間、入力を受け付けない時間が存在します。不具合ではありません。
- 全16ステージを順番に攻略する必要はありません。ステージ番号の順番に移動することは、たぶん、できません。
- ゲームクリア後、エンディングの最後に獲得した得点とクリアタイムが表示されます。ハイスコアを更新した場合は赤文字で表示されます。クリアタイムはタイトル画面でStartを選択すると同時に計測開始、エンディング画面が始まった瞬間に計測終了です。タイトル画面からContinueで開始した場合と全アイテム所有の裏技を使った場合はクリアしても計測結果は表示されません。
- タイトル画面のOptionsを選ぶとオプション画面に移動します。BGMとSEの音量がそれぞれ変更が可能です。変更した音量は保存され、次回起動時にも適用されます。
- フォントはPixelMplusを利用しています。
- このゲームはSHIRAISHIによって緊急事態宣言の間に製造が開始されました。製造期間はだいたい2か月です。

実行環境
=====
Windows 10の場合、ダウンロードしたzipファイルを右クリックして`プロパティ`から`ブロックの解除`にチェックを入れて`OK`押してください。その後、zipファイルを再び右クリックして`すべて展開`から`展開`を押してください。展開されたフォルダ内の`Game2.exe`をダブルクリックするとゲームが開始されます。

実行した結果パソコンが爆発したり、死人が出ても俺は知らん。

- 実行には各種OS向け[.NET Core Runtime 3.1.7](https://dotnet.microsoft.com/download/dotnet-core/3.1 ".NET Core Runtime 3.1.7")のインストールが必須です。
- LinuxとmacOSではシェルから`dotnet Game2.dll`で動きます。
