# About 

Smaregi API Explorer はスマレジ API のリクエストを簡単に組み立てテスト実行することが可能なWebアプリケーションです。 ＠sugimomoto が個人的に開発しているもので、株式会社スマレジとは一切関係ありません。

現在データの取得リクエストのみに対応しています。データの登録・更新リクエストには今後対応予定です。 Descriptionなどは公式のAPI Referenceを元に作っているので、全部が反映されているわけではないです。

# How to use

![howtouse](https://github.com/sugimomoto/SmaregiAPP.APIExplorer.Blazor/blob/master/SmaregiAPP.APIExplorer.Blazor/wwwroot/img/smaregi.gif?raw=true)

## STEP1. テーブルを選択してください。

対象テーブルを選択すると、取得できるカラムの一覧が表示されます。

## STEP2. 取得したい項目とフィルター条件を指定してください。

次に取得したいカラムのチェックボックスをONにしてください。

ConditionTypeとConditionValueを指定することで、取得するデータを絞り込むことが可能です。

なお、現在スマレジAPI側の制約により、ORG条件、NOR条件には対応していません。また、中には絞り込めないカラムもあります。

並び順はORDER BYで指定します。

また、デフォルトで100件のデータを取得できますが、Limitを変更することで最大1000件まで取得範囲を拡張できます。

ページネーションを行う場合は、PageのNoを指定してください。

## STEP3. 設定内容を確認し、Generateボタンをクリックしてください。Smaregi用リクエストデータが生成されます。

STEP2. までの情報を入力後、Genereateボタンをクリックすることで、スマレジAPIリクエストデータが生成されます。

Postmanや任意のプログラムで使用してください。

## STEP4. ContractIdとAccessTokenを指定することで、実際にSmaregi APIにアクセスできます。

もし、ContactIdとAccessTokenを持っている場合は、それぞれの項目に入力することで、簡易的に生成されたリクエストデータを試すことが可能です。

