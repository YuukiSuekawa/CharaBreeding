# CharaBreeding
たまごっちっぽい機能でキャラクターを地道に育てて遊ぶゲームです。 

![作成中の画像](https://scontent-nrt1-1.xx.fbcdn.net/v/t1.0-9/69108682_2964655817092933_8795317518618066944_n.jpg?_nc_cat=101&_nc_oc=AQkubwWLgixC4tL1a4E0RXEmej1hsb9IsjieArqenFLM-wj_Q26q1JaSdPSRy_xl8Kw&_nc_ht=scontent-nrt1-1.xx&oh=90a652ed7d00debdfced43a510166b9c&oe=5DD38E47)

## 動画サンプル(リンク先)
[![](https://raw.githubusercontent.com/YuukiSuekawa/TestRepository/master/2019-08-23_000750.png)](https://www.facebook.com/ys.1025/videos/2969753723249809/)


# 仕様
都度修正予定。  
基本的には別途マスターデータを作ってそれで効果値を変動可能にする予定。

## ご飯
時間の経過で満腹値が減っていきます。  
ご飯アクションでキャラクターにご飯を与えられます。  
満腹だとご飯アイコンが押せません。
+ 効果
  + 満腹度：30回復
  + 機嫌：10回復

## 睡眠
時間の経過で睡眠値が減っていきます。  
睡眠アクションで寝かせるか、睡眠値が0になると勝手に寝ます。  
睡眠値が最大だと睡眠アイコンが押せません。  
睡眠値が一定値に回復するまで他のアクションが出来ません。  
+ 効果
  + 眠気：一定時間後10回復
  + 機嫌：一定時間後5回復

## 機嫌
時間の経過で機嫌が悪くなります。  
機嫌アクションで遊んであげると機嫌が回復します。  
+ 効果
  + 機嫌：40回復

## 病気
うんこが室内にあると一定確率で病気にかかります。  
病気は放置しすぎるとキャラが死にます。  
病気の場合は病気アクションで治療できます。
+ 効果
  + 病気：健康

## 便意
便意が一定値たまると自動でうんこします。  
便意アクションでうんこを掃除できます。  
うんこが室内に一定以上貯まるとうんこを我慢して病気になりやすくなります。
+ 効果
  + うんこ清掃

# 構造(予定)
+ MainManager(シングルトン)
+ SoundManager(シングルトン)
+ SaveManager(シングルトン)
+ CharaBreedingManager：シーンのマネージャ想定
  + CharaManager
    + CharaController
    + CharaView
    + CharaModel
  + RoomManager
    + RoomController
    + RoomView
    + RoomModel
  + UIManager
