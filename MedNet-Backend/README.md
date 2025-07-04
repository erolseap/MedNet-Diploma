<!-- Generator: Widdershins v4.0.1 -->

<h1 id="mednet-webapi-v1">MedNet.WebApi | v1 v1.0.0</h1>

Base URLs:

* <a href="http://localhost:5000/">http://localhost:5000/</a>

<h1 id="mednet-webapi-v1-mednet-webapi">MedNet.WebApi</h1>

## post__account_register

> Code samples

`POST /account/register`

> Body parameter

```json
{
  "email": "string",
  "password": "string"
}
```

<h3 id="post__account_register-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|body|body|[RegisterRequest](#schemaregisterrequest)|true|none|

> Example responses

> 400 Response

```json
{
  "type": "string",
  "title": "string",
  "status": 0,
  "detail": "string",
  "instance": "string",
  "errors": {
    "property1": [
      "string"
    ],
    "property2": [
      "string"
    ]
  }
}
```

<h3 id="post__account_register-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|
|400|[Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)|Bad Request|[HttpValidationProblemDetails](#schemahttpvalidationproblemdetails)|


## post__account_login

> Code samples

`POST /account/login`

> Body parameter

```json
{
  "email": "string",
  "password": "string",
  "twoFactorCode": "string",
  "twoFactorRecoveryCode": "string"
}
```

<h3 id="post__account_login-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|useCookies|query|boolean|false|none|
|useSessionCookies|query|boolean|false|none|
|body|body|[LoginRequest](#schemaloginrequest)|true|none|

> Example responses

> 200 Response

```json
{
  "tokenType": "string",
  "accessToken": "string",
  "expiresIn": 0,
  "refreshToken": "string"
}
```

<h3 id="post__account_login-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|[AccessTokenResponse](#schemaaccesstokenresponse)|

## post__account_refresh

> Code samples

`POST /account/refresh`

> Body parameter

```json
{
  "refreshToken": "string"
}
```

<h3 id="post__account_refresh-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|body|body|[RefreshRequest](#schemarefreshrequest)|true|none|

> Example responses

> 200 Response

```json
{
  "tokenType": "string",
  "accessToken": "string",
  "expiresIn": 0,
  "refreshToken": "string"
}
```

<h3 id="post__account_refresh-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|[AccessTokenResponse](#schemaaccesstokenresponse)|


## MapIdentityApi-account_confirmEmail

<a id="opIdMapIdentityApi-account/confirmEmail"></a>

> Code samples

`GET /account/confirmEmail`

<h3 id="mapidentityapi-account_confirmemail-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|userId|query|string|true|none|
|code|query|string|true|none|
|changedEmail|query|string|false|none|

<h3 id="mapidentityapi-account_confirmemail-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|

## post__account_resendConfirmationEmail

> Code samples

`POST /account/resendConfirmationEmail`

> Body parameter

```json
{
  "email": "string"
}
```

<h3 id="post__account_resendconfirmationemail-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|body|body|[ResendConfirmationEmailRequest](#schemaresendconfirmationemailrequest)|true|none|

<h3 id="post__account_resendconfirmationemail-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|

## post__account_forgotPassword

> Code samples

`POST /account/forgotPassword`

> Body parameter

```json
{
  "email": "string"
}
```

<h3 id="post__account_forgotpassword-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|body|body|[ForgotPasswordRequest](#schemaforgotpasswordrequest)|true|none|

> Example responses

> 400 Response

```json
{
  "type": "string",
  "title": "string",
  "status": 0,
  "detail": "string",
  "instance": "string",
  "errors": {
    "property1": [
      "string"
    ],
    "property2": [
      "string"
    ]
  }
}
```

<h3 id="post__account_forgotpassword-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|
|400|[Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)|Bad Request|[HttpValidationProblemDetails](#schemahttpvalidationproblemdetails)|

## post__account_resetPassword

> Code samples

`POST /account/resetPassword`

> Body parameter

```json
{
  "email": "string",
  "resetCode": "string",
  "newPassword": "string"
}
```

<h3 id="post__account_resetpassword-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|body|body|[ResetPasswordRequest](#schemaresetpasswordrequest)|true|none|

> Example responses

> 400 Response

```json
{
  "type": "string",
  "title": "string",
  "status": 0,
  "detail": "string",
  "instance": "string",
  "errors": {
    "property1": [
      "string"
    ],
    "property2": [
      "string"
    ]
  }
}
```

<h3 id="post__account_resetpassword-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|
|400|[Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)|Bad Request|[HttpValidationProblemDetails](#schemahttpvalidationproblemdetails)|

## post__account_manage_2fa

> Code samples

`POST /account/manage/2fa`

> Body parameter

```json
{
  "enable": true,
  "twoFactorCode": "string",
  "resetSharedKey": true,
  "resetRecoveryCodes": true,
  "forgetMachine": true
}
```

<h3 id="post__account_manage_2fa-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|body|body|[TwoFactorRequest](#schematwofactorrequest)|true|none|

> Example responses

> 200 Response

```json
{
  "sharedKey": "string",
  "recoveryCodesLeft": 0,
  "recoveryCodes": [
    "string"
  ],
  "isTwoFactorEnabled": true,
  "isMachineRemembered": true
}
```

<h3 id="post__account_manage_2fa-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|[TwoFactorResponse](#schematwofactorresponse)|
|400|[Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)|Bad Request|[HttpValidationProblemDetails](#schemahttpvalidationproblemdetails)|
|404|[Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4)|Not Found|None|

## get__account_manage_info

> Code samples

`GET /account/manage/info`

> Example responses

> 200 Response

```json
{
  "email": "string",
  "isEmailConfirmed": true
}
```

<h3 id="get__account_manage_info-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|[InfoResponse](#schemainforesponse)|
|400|[Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)|Bad Request|[HttpValidationProblemDetails](#schemahttpvalidationproblemdetails)|
|404|[Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4)|Not Found|None|

## post__account_manage_info

> Code samples

`POST /account/manage/info`

> Body parameter

```json
{
  "newEmail": "string",
  "newPassword": "string",
  "oldPassword": "string"
}
```

<h3 id="post__account_manage_info-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|body|body|[InfoRequest](#schemainforequest)|true|none|

> Example responses

> 200 Response

```json
{
  "email": "string",
  "isEmailConfirmed": true
}
```

<h3 id="post__account_manage_info-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|[InfoResponse](#schemainforesponse)|
|400|[Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)|Bad Request|[HttpValidationProblemDetails](#schemahttpvalidationproblemdetails)|
|404|[Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4)|Not Found|None|

<h1 id="mednet-webapi-v1-usertest">UserTest</h1>

## List all test questions

<a id="opIdList all test questions"></a>

> Code samples

`GET /my-tests/{id}/questions`

<h3 id="list-all-test-questions-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|id|path|integer(int32)|true|none|

> Example responses

> 200 Response

```
[{"id":0,"body":"string","blankQuestionNumber":0,"answers":[{"id":0,"body":"string"}],"selectedAnswerId":0,"correctAnswerId":0}]
```

```json
[
  {
    "id": 0,
    "body": "string",
    "blankQuestionNumber": 0,
    "answers": [
      {
        "id": 0,
        "body": "string"
      }
    ],
    "selectedAnswerId": 0,
    "correctAnswerId": 0
  }
]
```

<h3 id="list-all-test-questions-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|Inline|
|400|[Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)|Bad Request|[ProblemDetails](#schemaproblemdetails)|
|404|[Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4)|Not Found|[ProblemDetails](#schemaproblemdetails)|

<h3 id="list-all-test-questions-responseschema">Response Schema</h3>

Status Code **200**

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|*anonymous*|[[UserTestSessionQuestionDto](#schemausertestsessionquestiondto)]|false|none|none|
|» id|integer(int32)|true|none|none|
|» body|string|true|none|none|
|» blankQuestionNumber|integer(int32)|true|none|none|
|» answers|[[AnswerWithoutStatusDto](#schemaanswerwithoutstatusdto)]|true|none|none|
|»» id|integer(int32)|true|none|none|
|»» body|string|true|none|none|
|» selectedAnswerId|integer(int32)¦null|true|none|none|
|» correctAnswerId|integer(int32)¦null|true|none|none|

<h1 id="mednet-webapi-v1-usertestquestion">UserTestQuestion</h1>

## Get a specific test question

<a id="opIdGet a specific test question"></a>

> Code samples

`GET /my-tests/{parentid}/questions/{id}`

<h3 id="get-a-specific-test-question-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|parentid|path|integer(int32)|true|none|
|id|path|integer(int32)|true|none|

> Example responses

> 200 Response

```
{"id":0,"body":"string","blankQuestionNumber":0,"answers":[{"id":0,"body":"string"}],"selectedAnswerId":0,"correctAnswerId":0}
```

```json
{
  "id": 0,
  "body": "string",
  "blankQuestionNumber": 0,
  "answers": [
    {
      "id": 0,
      "body": "string"
    }
  ],
  "selectedAnswerId": 0,
  "correctAnswerId": 0
}
```

<h3 id="get-a-specific-test-question-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|[UserTestSessionQuestionDto](#schemausertestsessionquestiondto)|
|400|[Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)|Bad Request|[ProblemDetails](#schemaproblemdetails)|
|404|[Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4)|Not Found|[ProblemDetails](#schemaproblemdetails)|

## Answer a specific test question

<a id="opIdAnswer a specific test question"></a>

> Code samples

`POST /my-tests/{parentid}/questions/{id}/answer`

> Body parameter

```json
{
  "answerId": 0
}
```

<h3 id="answer-a-specific-test-question-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|parentid|path|integer(int32)|true|none|
|id|path|integer(int32)|true|none|
|body|body|[UserTestQuestionControllerAnswerDto](#schemausertestquestioncontrolleranswerdto)|true|none|

> Example responses

> 400 Response

```
{"type":"string","title":"string","status":0,"detail":"string","instance":"string"}
```

```json
{
  "type": "string",
  "title": "string",
  "status": 0,
  "detail": "string",
  "instance": "string"
}
```

<h3 id="answer-a-specific-test-question-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|
|400|[Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)|Bad Request|[ProblemDetails](#schemaproblemdetails)|
|404|[Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4)|Not Found|[ProblemDetails](#schemaproblemdetails)|

<h1 id="mednet-webapi-v1-usertests">UserTests</h1>

## List all my generated tests

<a id="opIdList all my generated tests"></a>

> Code samples

`GET /my-tests`

> Example responses

> 200 Response

```
[{"id":0,"parentQuestionsSetId":0,"creationDate":"2019-08-24T14:15:22Z","numOfQuestions":0,"correctAnswersCount":0}]
```

```json
[
  {
    "id": 0,
    "parentQuestionsSetId": 0,
    "creationDate": "2019-08-24T14:15:22Z",
    "numOfQuestions": 0,
    "correctAnswersCount": 0
  }
]
```

<h3 id="list-all-my-generated-tests-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|Inline|
|403|[Forbidden](https://tools.ietf.org/html/rfc7231#section-6.5.3)|Forbidden|[ProblemDetails](#schemaproblemdetails)|

<h3 id="list-all-my-generated-tests-responseschema">Response Schema</h3>

Status Code **200**

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|*anonymous*|[[UserTestSessionDto](#schemausertestsessiondto)]|false|none|none|
|» id|integer(int32)|true|none|none|
|» parentQuestionsSetId|integer(int32)|true|none|none|
|» creationDate|string(date-time)|true|none|none|
|» numOfQuestions|integer(int32)|true|none|none|
|» correctAnswersCount|integer(int32)|true|none|none|

## Get a list of generatable tests

<a id="opIdGet a list of generatable tests"></a>

> Code samples

`GET /my-tests/generatable`

<h3 id="get-a-list-of-generatable-tests-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|Offset|query|integer(int32)|false|none|
|Limit|query|integer(int32)|false|none|

> Example responses

> 200 Response

```
[{"id":0,"name":"string","numOfQuestions":0}]
```

```json
[
  {
    "id": 0,
    "name": "string",
    "numOfQuestions": 0
  }
]
```

<h3 id="get-a-list-of-generatable-tests-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|Inline|
|403|[Forbidden](https://tools.ietf.org/html/rfc7231#section-6.5.3)|Forbidden|[ProblemDetails](#schemaproblemdetails)|

<h3 id="get-a-list-of-generatable-tests-responseschema">Response Schema</h3>

Status Code **200**

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|*anonymous*|[[QuestionsSetWithNumOfQuestionsDto](#schemaquestionssetwithnumofquestionsdto)]|false|none|none|
|» id|integer(int32)|true|none|none|
|» name|string|true|none|none|
|» numOfQuestions|integer(int32)|true|none|none|

## Generate a new test

<a id="opIdGenerate a new test"></a>

> Code samples

`POST /my-tests/generate`

> Body parameter

```json
{
  "setId": 0,
  "numOfQuestions": 0
}
```

<h3 id="generate-a-new-test-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|body|body|[UserTestsControllerGenerateDto](#schemausertestscontrollergeneratedto)|true|none|

> Example responses

> 201 Response

```
{"id":0,"parentQuestionsSetId":0,"creationDate":"2019-08-24T14:15:22Z","numOfQuestions":0,"correctAnswersCount":0}
```

```json
{
  "id": 0,
  "parentQuestionsSetId": 0,
  "creationDate": "2019-08-24T14:15:22Z",
  "numOfQuestions": 0,
  "correctAnswersCount": 0
}
```

<h3 id="generate-a-new-test-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|201|[Created](https://tools.ietf.org/html/rfc7231#section-6.3.2)|Created|[UserTestSessionDto](#schemausertestsessiondto)|
|403|[Forbidden](https://tools.ietf.org/html/rfc7231#section-6.5.3)|Forbidden|[ProblemDetails](#schemaproblemdetails)|

<h1 id="mednet-webapi-v1-question">Question</h1>

## Get a specific question

<a id="opIdGet a specific question"></a>

> Code samples

`GET /admin/sets/{parentid}/questions/{id}`

<h3 id="get-a-specific-question-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|parentid|path|integer(int32)|true|none|
|id|path|integer(int32)|true|none|

> Example responses

> 200 Response

```
{"id":0,"body":"string","blankQuestionNumber":0,"answers":[{"isCorrect":true,"id":0,"body":"string"}]}
```

```json
{
  "id": 0,
  "body": "string",
  "blankQuestionNumber": 0,
  "answers": [
    {
      "isCorrect": true,
      "id": 0,
      "body": "string"
    }
  ]
}
```

<h3 id="get-a-specific-question-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|[QuestionDto](#schemaquestiondto)|
|400|[Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)|Bad Request|[ProblemDetails](#schemaproblemdetails)|
|404|[Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4)|Not Found|[ProblemDetails](#schemaproblemdetails)|

## Update a specific question

<a id="opIdUpdate a specific question"></a>

> Code samples

`PATCH /admin/sets/{parentid}/questions/{id}`

> Body parameter

```json
{
  "body": "string",
  "answers": [
    {
      "isCorrect": true,
      "id": 0,
      "body": "string"
    }
  ]
}
```

<h3 id="update-a-specific-question-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|parentid|path|integer(int32)|true|none|
|id|path|integer(int32)|true|none|
|body|body|[QuestionControllerUpdateDto](#schemaquestioncontrollerupdatedto)|true|none|

> Example responses

> 400 Response

```
{"type":"string","title":"string","status":0,"detail":"string","instance":"string"}
```

```json
{
  "type": "string",
  "title": "string",
  "status": 0,
  "detail": "string",
  "instance": "string"
}
```

<h3 id="update-a-specific-question-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|
|400|[Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)|Bad Request|[ProblemDetails](#schemaproblemdetails)|
|404|[Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4)|Not Found|[ProblemDetails](#schemaproblemdetails)|

## Delete a specific question

<a id="opIdDelete a specific question"></a>

> Code samples

`DELETE /admin/sets/{parentid}/questions/{id}`

<h3 id="delete-a-specific-question-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|parentid|path|integer(int32)|true|none|
|id|path|integer(int32)|true|none|

> Example responses

> 400 Response

```
{"type":"string","title":"string","status":0,"detail":"string","instance":"string"}
```

```json
{
  "type": "string",
  "title": "string",
  "status": 0,
  "detail": "string",
  "instance": "string"
}
```

<h3 id="delete-a-specific-question-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|
|400|[Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)|Bad Request|[ProblemDetails](#schemaproblemdetails)|
|404|[Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4)|Not Found|[ProblemDetails](#schemaproblemdetails)|

<h1 id="mednet-webapi-v1-questionsset">QuestionsSet</h1>

## Get a specific questions set

<a id="opIdGet a specific questions set"></a>

> Code samples

`GET /admin/sets/{id}`

<h3 id="get-a-specific-questions-set-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|id|path|integer(int32)|true|none|

> Example responses

> 200 Response

```
{"id":0,"name":"string","numOfQuestions":0}
```

```json
{
  "id": 0,
  "name": "string",
  "numOfQuestions": 0
}
```

<h3 id="get-a-specific-questions-set-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|[QuestionsSetWithNumOfQuestionsDto](#schemaquestionssetwithnumofquestionsdto)|
|400|[Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)|Bad Request|[ProblemDetails](#schemaproblemdetails)|
|404|[Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4)|Not Found|[ProblemDetails](#schemaproblemdetails)|

## Update a specific questions set

<a id="opIdUpdate a specific questions set"></a>

> Code samples

`PATCH /admin/sets/{id}`

> Body parameter

```json
{
  "name": "string"
}
```

<h3 id="update-a-specific-questions-set-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|id|path|integer(int32)|true|none|
|body|body|[QuestionsSetControllerUpdateDto](#schemaquestionssetcontrollerupdatedto)|true|none|

> Example responses

> 400 Response

```
{"type":"string","title":"string","status":0,"detail":"string","instance":"string"}
```

```json
{
  "type": "string",
  "title": "string",
  "status": 0,
  "detail": "string",
  "instance": "string"
}
```

<h3 id="update-a-specific-questions-set-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|
|400|[Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)|Bad Request|[ProblemDetails](#schemaproblemdetails)|
|404|[Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4)|Not Found|[ProblemDetails](#schemaproblemdetails)|

## Delete a specific questions set

<a id="opIdDelete a specific questions set"></a>

> Code samples

`DELETE /admin/sets/{id}`

<h3 id="delete-a-specific-questions-set-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|id|path|integer(int32)|true|none|

> Example responses

> 400 Response

```
{"type":"string","title":"string","status":0,"detail":"string","instance":"string"}
```

```json
{
  "type": "string",
  "title": "string",
  "status": 0,
  "detail": "string",
  "instance": "string"
}
```

<h3 id="delete-a-specific-questions-set-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|
|400|[Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)|Bad Request|[ProblemDetails](#schemaproblemdetails)|
|404|[Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4)|Not Found|[ProblemDetails](#schemaproblemdetails)|

## List all questions

<a id="opIdList all questions"></a>

> Code samples

`GET /admin/sets/{id}/questions`

<h3 id="list-all-questions-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|Offset|query|integer(int32)|false|none|
|Limit|query|integer(int32)|false|none|
|id|path|integer(int32)|true|none|

> Example responses

> 200 Response

```
[{"id":0,"body":"string","blankQuestionNumber":0,"answers":[{"isCorrect":true,"id":0,"body":"string"}]}]
```

```json
[
  {
    "id": 0,
    "body": "string",
    "blankQuestionNumber": 0,
    "answers": [
      {
        "isCorrect": true,
        "id": 0,
        "body": "string"
      }
    ]
  }
]
```

<h3 id="list-all-questions-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|Inline|
|400|[Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)|Bad Request|[ProblemDetails](#schemaproblemdetails)|
|404|[Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4)|Not Found|[ProblemDetails](#schemaproblemdetails)|

<h3 id="list-all-questions-responseschema">Response Schema</h3>

Status Code **200**

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|*anonymous*|[[QuestionDto](#schemaquestiondto)]|false|none|none|
|» id|integer(int32)|true|none|none|
|» body|string|true|none|none|
|» blankQuestionNumber|integer(int32)|true|none|none|
|» answers|[[AnswerDto](#schemaanswerdto)]|true|none|none|
|»» isCorrect|boolean|true|none|none|
|»» id|integer(int32)|true|none|none|
|»» body|string|true|none|none|

## Create a new question

<a id="opIdCreate a new question"></a>

> Code samples

`POST /admin/sets/{id}/questions`

> Body parameter

```json
{
  "body": "string",
  "blankQuestionNumber": 0,
  "answers": [
    {
      "isCorrect": true,
      "id": 0,
      "body": "string"
    }
  ]
}
```

<h3 id="create-a-new-question-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|id|path|integer(int32)|true|none|
|body|body|[QuestionsSetControllerCreateQuestionDto](#schemaquestionssetcontrollercreatequestiondto)|true|none|

> Example responses

> 201 Response

```
{"id":0,"body":"string","blankQuestionNumber":0,"answers":[{"isCorrect":true,"id":0,"body":"string"}]}
```

```json
{
  "id": 0,
  "body": "string",
  "blankQuestionNumber": 0,
  "answers": [
    {
      "isCorrect": true,
      "id": 0,
      "body": "string"
    }
  ]
}
```

<h3 id="create-a-new-question-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|201|[Created](https://tools.ietf.org/html/rfc7231#section-6.3.2)|Created|[QuestionDto](#schemaquestiondto)|
|400|[Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)|Bad Request|[ProblemDetails](#schemaproblemdetails)|
|404|[Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4)|Not Found|[ProblemDetails](#schemaproblemdetails)|

## Create a new questions (batch)

<a id="opIdCreate a new questions (batch)"></a>

> Code samples

`POST /admin/sets/{id}/questions/batch`

> Body parameter

```json
[
  {
    "body": "string",
    "blankQuestionNumber": 0,
    "answers": [
      {
        "isCorrect": true,
        "id": 0,
        "body": "string"
      }
    ]
  }
]
```

<h3 id="create-a-new-questions-(batch)-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|id|path|integer(int32)|true|none|
|body|body|[QuestionsSetControllerCreateQuestionDto](#schemaquestionssetcontrollercreatequestiondto)|true|none|

> Example responses

> 400 Response

```
{"type":"string","title":"string","status":0,"detail":"string","instance":"string"}
```

```json
{
  "type": "string",
  "title": "string",
  "status": 0,
  "detail": "string",
  "instance": "string"
}
```

<h3 id="create-a-new-questions-(batch)-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|201|[Created](https://tools.ietf.org/html/rfc7231#section-6.3.2)|Created|None|
|400|[Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)|Bad Request|[ProblemDetails](#schemaproblemdetails)|
|404|[Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4)|Not Found|[ProblemDetails](#schemaproblemdetails)|

<h1 id="mednet-webapi-v1-questionssets">QuestionsSets</h1>

## List all questions sets

<a id="opIdList all questions sets"></a>

> Code samples

`GET /admin/sets`

<h3 id="list-all-questions-sets-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|Offset|query|integer(int32)|false|none|
|Limit|query|integer(int32)|false|none|

> Example responses

> 200 Response

```
[{"id":0,"name":"string","numOfQuestions":0}]
```

```json
[
  {
    "id": 0,
    "name": "string",
    "numOfQuestions": 0
  }
]
```

<h3 id="list-all-questions-sets-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|Inline|
|403|[Forbidden](https://tools.ietf.org/html/rfc7231#section-6.5.3)|Forbidden|[ProblemDetails](#schemaproblemdetails)|

<h3 id="list-all-questions-sets-responseschema">Response Schema</h3>

Status Code **200**

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|*anonymous*|[[QuestionsSetWithNumOfQuestionsDto](#schemaquestionssetwithnumofquestionsdto)]|false|none|none|
|» id|integer(int32)|true|none|none|
|» name|string|true|none|none|
|» numOfQuestions|integer(int32)|true|none|none|

## Create an empty questions set

<a id="opIdCreate an empty questions set"></a>

> Code samples

`POST /admin/sets`

> Body parameter

```json
{
  "name": "string"
}
```

<h3 id="create-an-empty-questions-set-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|body|body|[CreateQuestionsSetCommand](#schemacreatequestionssetcommand)|true|none|

> Example responses

> 201 Response

```
{"id":0,"name":"string","numOfQuestions":0}
```

```json
{
  "id": 0,
  "name": "string",
  "numOfQuestions": 0
}
```

<h3 id="create-an-empty-questions-set-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|201|[Created](https://tools.ietf.org/html/rfc7231#section-6.3.2)|Created|[QuestionsSetWithNumOfQuestionsDto](#schemaquestionssetwithnumofquestionsdto)|
|403|[Forbidden](https://tools.ietf.org/html/rfc7231#section-6.5.3)|Forbidden|[ProblemDetails](#schemaproblemdetails)|

# Schemas

<h2 id="tocS_AccessTokenResponse">AccessTokenResponse</h2>
<!-- backwards compatibility -->
<a id="schemaaccesstokenresponse"></a>
<a id="schema_AccessTokenResponse"></a>
<a id="tocSaccesstokenresponse"></a>
<a id="tocsaccesstokenresponse"></a>

```json
{
  "tokenType": "string",
  "accessToken": "string",
  "expiresIn": 0,
  "refreshToken": "string"
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|tokenType|string¦null|false|none|none|
|accessToken|string|true|none|none|
|expiresIn|integer(int64)|true|none|none|
|refreshToken|string|true|none|none|

<h2 id="tocS_AnswerDto">AnswerDto</h2>
<!-- backwards compatibility -->
<a id="schemaanswerdto"></a>
<a id="schema_AnswerDto"></a>
<a id="tocSanswerdto"></a>
<a id="tocsanswerdto"></a>

```json
{
  "isCorrect": true,
  "id": 0,
  "body": "string"
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|isCorrect|boolean|true|none|none|
|id|integer(int32)|true|none|none|
|body|string|true|none|none|

<h2 id="tocS_AnswerWithoutStatusDto">AnswerWithoutStatusDto</h2>
<!-- backwards compatibility -->
<a id="schemaanswerwithoutstatusdto"></a>
<a id="schema_AnswerWithoutStatusDto"></a>
<a id="tocSanswerwithoutstatusdto"></a>
<a id="tocsanswerwithoutstatusdto"></a>

```json
{
  "id": 0,
  "body": "string"
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|id|integer(int32)|true|none|none|
|body|string|true|none|none|

<h2 id="tocS_CreateQuestionsSetCommand">CreateQuestionsSetCommand</h2>
<!-- backwards compatibility -->
<a id="schemacreatequestionssetcommand"></a>
<a id="schema_CreateQuestionsSetCommand"></a>
<a id="tocScreatequestionssetcommand"></a>
<a id="tocscreatequestionssetcommand"></a>

```json
{
  "name": "string"
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|name|string|true|none|none|

<h2 id="tocS_ForgotPasswordRequest">ForgotPasswordRequest</h2>
<!-- backwards compatibility -->
<a id="schemaforgotpasswordrequest"></a>
<a id="schema_ForgotPasswordRequest"></a>
<a id="tocSforgotpasswordrequest"></a>
<a id="tocsforgotpasswordrequest"></a>

```json
{
  "email": "string"
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|email|string|true|none|none|

<h2 id="tocS_HttpValidationProblemDetails">HttpValidationProblemDetails</h2>
<!-- backwards compatibility -->
<a id="schemahttpvalidationproblemdetails"></a>
<a id="schema_HttpValidationProblemDetails"></a>
<a id="tocShttpvalidationproblemdetails"></a>
<a id="tocshttpvalidationproblemdetails"></a>

```json
{
  "type": "string",
  "title": "string",
  "status": 0,
  "detail": "string",
  "instance": "string",
  "errors": {
    "property1": [
      "string"
    ],
    "property2": [
      "string"
    ]
  }
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|type|string¦null|false|none|none|
|title|string¦null|false|none|none|
|status|integer(int32)¦null|false|none|none|
|detail|string¦null|false|none|none|
|instance|string¦null|false|none|none|
|errors|object|false|none|none|
|» **additionalProperties**|[string]|false|none|none|

<h2 id="tocS_InfoRequest">InfoRequest</h2>
<!-- backwards compatibility -->
<a id="schemainforequest"></a>
<a id="schema_InfoRequest"></a>
<a id="tocSinforequest"></a>
<a id="tocsinforequest"></a>

```json
{
  "newEmail": "string",
  "newPassword": "string",
  "oldPassword": "string"
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|newEmail|string¦null|false|none|none|
|newPassword|string¦null|false|none|none|
|oldPassword|string¦null|false|none|none|

<h2 id="tocS_InfoResponse">InfoResponse</h2>
<!-- backwards compatibility -->
<a id="schemainforesponse"></a>
<a id="schema_InfoResponse"></a>
<a id="tocSinforesponse"></a>
<a id="tocsinforesponse"></a>

```json
{
  "email": "string",
  "isEmailConfirmed": true
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|email|string|true|none|none|
|isEmailConfirmed|boolean|true|none|none|

<h2 id="tocS_LoginRequest">LoginRequest</h2>
<!-- backwards compatibility -->
<a id="schemaloginrequest"></a>
<a id="schema_LoginRequest"></a>
<a id="tocSloginrequest"></a>
<a id="tocsloginrequest"></a>

```json
{
  "email": "string",
  "password": "string",
  "twoFactorCode": "string",
  "twoFactorRecoveryCode": "string"
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|email|string|true|none|none|
|password|string|true|none|none|
|twoFactorCode|string¦null|false|none|none|
|twoFactorRecoveryCode|string¦null|false|none|none|

<h2 id="tocS_ProblemDetails">ProblemDetails</h2>
<!-- backwards compatibility -->
<a id="schemaproblemdetails"></a>
<a id="schema_ProblemDetails"></a>
<a id="tocSproblemdetails"></a>
<a id="tocsproblemdetails"></a>

```json
{
  "type": "string",
  "title": "string",
  "status": 0,
  "detail": "string",
  "instance": "string"
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|type|string¦null|false|none|none|
|title|string¦null|false|none|none|
|status|integer(int32)¦null|false|none|none|
|detail|string¦null|false|none|none|
|instance|string¦null|false|none|none|

<h2 id="tocS_QuestionControllerUpdateDto">QuestionControllerUpdateDto</h2>
<!-- backwards compatibility -->
<a id="schemaquestioncontrollerupdatedto"></a>
<a id="schema_QuestionControllerUpdateDto"></a>
<a id="tocSquestioncontrollerupdatedto"></a>
<a id="tocsquestioncontrollerupdatedto"></a>

```json
{
  "body": "string",
  "answers": [
    {
      "isCorrect": true,
      "id": 0,
      "body": "string"
    }
  ]
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|body|string¦null|false|none|none|
|answers|[[AnswerDto](#schemaanswerdto)]¦null|false|none|none|

<h2 id="tocS_QuestionDto">QuestionDto</h2>
<!-- backwards compatibility -->
<a id="schemaquestiondto"></a>
<a id="schema_QuestionDto"></a>
<a id="tocSquestiondto"></a>
<a id="tocsquestiondto"></a>

```json
{
  "id": 0,
  "body": "string",
  "blankQuestionNumber": 0,
  "answers": [
    {
      "isCorrect": true,
      "id": 0,
      "body": "string"
    }
  ]
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|id|integer(int32)|true|none|none|
|body|string|true|none|none|
|blankQuestionNumber|integer(int32)|true|none|none|
|answers|[[AnswerDto](#schemaanswerdto)]|true|none|none|

<h2 id="tocS_QuestionsSetControllerCreateQuestionDto">QuestionsSetControllerCreateQuestionDto</h2>
<!-- backwards compatibility -->
<a id="schemaquestionssetcontrollercreatequestiondto"></a>
<a id="schema_QuestionsSetControllerCreateQuestionDto"></a>
<a id="tocSquestionssetcontrollercreatequestiondto"></a>
<a id="tocsquestionssetcontrollercreatequestiondto"></a>

```json
{
  "body": "string",
  "blankQuestionNumber": 0,
  "answers": [
    {
      "isCorrect": true,
      "id": 0,
      "body": "string"
    }
  ]
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|body|string¦null|true|none|none|
|blankQuestionNumber|integer(int32)|false|none|none|
|answers|[[AnswerDto](#schemaanswerdto)]|false|none|none|

<h2 id="tocS_QuestionsSetControllerUpdateDto">QuestionsSetControllerUpdateDto</h2>
<!-- backwards compatibility -->
<a id="schemaquestionssetcontrollerupdatedto"></a>
<a id="schema_QuestionsSetControllerUpdateDto"></a>
<a id="tocSquestionssetcontrollerupdatedto"></a>
<a id="tocsquestionssetcontrollerupdatedto"></a>

```json
{
  "name": "string"
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|name|string¦null|false|none|none|

<h2 id="tocS_QuestionsSetWithNumOfQuestionsDto">QuestionsSetWithNumOfQuestionsDto</h2>
<!-- backwards compatibility -->
<a id="schemaquestionssetwithnumofquestionsdto"></a>
<a id="schema_QuestionsSetWithNumOfQuestionsDto"></a>
<a id="tocSquestionssetwithnumofquestionsdto"></a>
<a id="tocsquestionssetwithnumofquestionsdto"></a>

```json
{
  "id": 0,
  "name": "string",
  "numOfQuestions": 0
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|id|integer(int32)|true|none|none|
|name|string|true|none|none|
|numOfQuestions|integer(int32)|true|none|none|

<h2 id="tocS_RefreshRequest">RefreshRequest</h2>
<!-- backwards compatibility -->
<a id="schemarefreshrequest"></a>
<a id="schema_RefreshRequest"></a>
<a id="tocSrefreshrequest"></a>
<a id="tocsrefreshrequest"></a>

```json
{
  "refreshToken": "string"
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|refreshToken|string|true|none|none|

<h2 id="tocS_RegisterRequest">RegisterRequest</h2>
<!-- backwards compatibility -->
<a id="schemaregisterrequest"></a>
<a id="schema_RegisterRequest"></a>
<a id="tocSregisterrequest"></a>
<a id="tocsregisterrequest"></a>

```json
{
  "email": "string",
  "password": "string"
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|email|string|true|none|none|
|password|string|true|none|none|

<h2 id="tocS_ResendConfirmationEmailRequest">ResendConfirmationEmailRequest</h2>
<!-- backwards compatibility -->
<a id="schemaresendconfirmationemailrequest"></a>
<a id="schema_ResendConfirmationEmailRequest"></a>
<a id="tocSresendconfirmationemailrequest"></a>
<a id="tocsresendconfirmationemailrequest"></a>

```json
{
  "email": "string"
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|email|string|true|none|none|

<h2 id="tocS_ResetPasswordRequest">ResetPasswordRequest</h2>
<!-- backwards compatibility -->
<a id="schemaresetpasswordrequest"></a>
<a id="schema_ResetPasswordRequest"></a>
<a id="tocSresetpasswordrequest"></a>
<a id="tocsresetpasswordrequest"></a>

```json
{
  "email": "string",
  "resetCode": "string",
  "newPassword": "string"
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|email|string|true|none|none|
|resetCode|string|true|none|none|
|newPassword|string|true|none|none|

<h2 id="tocS_TwoFactorRequest">TwoFactorRequest</h2>
<!-- backwards compatibility -->
<a id="schematwofactorrequest"></a>
<a id="schema_TwoFactorRequest"></a>
<a id="tocStwofactorrequest"></a>
<a id="tocstwofactorrequest"></a>

```json
{
  "enable": true,
  "twoFactorCode": "string",
  "resetSharedKey": true,
  "resetRecoveryCodes": true,
  "forgetMachine": true
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|enable|boolean¦null|false|none|none|
|twoFactorCode|string¦null|false|none|none|
|resetSharedKey|boolean|false|none|none|
|resetRecoveryCodes|boolean|false|none|none|
|forgetMachine|boolean|false|none|none|

<h2 id="tocS_TwoFactorResponse">TwoFactorResponse</h2>
<!-- backwards compatibility -->
<a id="schematwofactorresponse"></a>
<a id="schema_TwoFactorResponse"></a>
<a id="tocStwofactorresponse"></a>
<a id="tocstwofactorresponse"></a>

```json
{
  "sharedKey": "string",
  "recoveryCodesLeft": 0,
  "recoveryCodes": [
    "string"
  ],
  "isTwoFactorEnabled": true,
  "isMachineRemembered": true
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|sharedKey|string|true|none|none|
|recoveryCodesLeft|integer(int32)|true|none|none|
|recoveryCodes|[string]¦null|false|none|none|
|isTwoFactorEnabled|boolean|true|none|none|
|isMachineRemembered|boolean|true|none|none|

<h2 id="tocS_UserTestQuestionControllerAnswerDto">UserTestQuestionControllerAnswerDto</h2>
<!-- backwards compatibility -->
<a id="schemausertestquestioncontrolleranswerdto"></a>
<a id="schema_UserTestQuestionControllerAnswerDto"></a>
<a id="tocSusertestquestioncontrolleranswerdto"></a>
<a id="tocsusertestquestioncontrolleranswerdto"></a>

```json
{
  "answerId": 0
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|answerId|integer(int32)¦null|true|none|none|

<h2 id="tocS_UserTestsControllerGenerateDto">UserTestsControllerGenerateDto</h2>
<!-- backwards compatibility -->
<a id="schemausertestscontrollergeneratedto"></a>
<a id="schema_UserTestsControllerGenerateDto"></a>
<a id="tocSusertestscontrollergeneratedto"></a>
<a id="tocsusertestscontrollergeneratedto"></a>

```json
{
  "setId": 0,
  "numOfQuestions": 0
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|setId|integer(int32)¦null|true|none|none|
|numOfQuestions|integer(int32)|false|none|none|

<h2 id="tocS_UserTestSessionDto">UserTestSessionDto</h2>
<!-- backwards compatibility -->
<a id="schemausertestsessiondto"></a>
<a id="schema_UserTestSessionDto"></a>
<a id="tocSusertestsessiondto"></a>
<a id="tocsusertestsessiondto"></a>

```json
{
  "id": 0,
  "parentQuestionsSetId": 0,
  "creationDate": "2019-08-24T14:15:22Z",
  "numOfQuestions": 0,
  "correctAnswersCount": 0
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|id|integer(int32)|true|none|none|
|parentQuestionsSetId|integer(int32)|true|none|none|
|creationDate|string(date-time)|true|none|none|
|numOfQuestions|integer(int32)|true|none|none|
|correctAnswersCount|integer(int32)|true|none|none|

<h2 id="tocS_UserTestSessionQuestionDto">UserTestSessionQuestionDto</h2>
<!-- backwards compatibility -->
<a id="schemausertestsessionquestiondto"></a>
<a id="schema_UserTestSessionQuestionDto"></a>
<a id="tocSusertestsessionquestiondto"></a>
<a id="tocsusertestsessionquestiondto"></a>

```json
{
  "id": 0,
  "body": "string",
  "blankQuestionNumber": 0,
  "answers": [
    {
      "id": 0,
      "body": "string"
    }
  ],
  "selectedAnswerId": 0,
  "correctAnswerId": 0
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|id|integer(int32)|true|none|none|
|body|string|true|none|none|
|blankQuestionNumber|integer(int32)|true|none|none|
|answers|[[AnswerWithoutStatusDto](#schemaanswerwithoutstatusdto)]|true|none|none|
|selectedAnswerId|integer(int32)¦null|true|none|none|
|correctAnswerId|integer(int32)¦null|true|none|none|

