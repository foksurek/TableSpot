# TableSpot
### API Reference
###### to be updated

### /api/Restaurant/GetAll

#### GET
##### Parameters

| Name    | Located in | Required | Schema   |
|---------|------------|----------|----------|
| limit   | query      | No       | integer  |
| offset  | query      | No       | integer  |

##### Responses

| Code  | Description |
|-------|-------------|
| 200   | Success     |

### /api/Restaurant/GetById

#### GET
##### Parameters

| Name | Located in | Required | Schema  |
|------|------------|----------|---------|
| id   | query      | Yes      | integer |

##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Success |

### /api/Restaurant/GetByName

#### GET
##### Parameters

| Name    | Located in | Required | Schema   |
|---------|------------|----------|----------|
| name    |  query     | Yes      | string   |
| limit   | query      | No       | inte ger |
| offset  | query      | No       | integer  |

##### Responses

| Code  | Description |
|-------|-------------|
| 200   | Success     |

### /api/Restaurant/GetByCategory

#### GET
##### Parameters

| Name       | Located in | Required | Schema  |
|------------|------------|----------|---------|
| categoryId | query      | Yes      | integer |
| limit      | query      |  No      | integer |
| offset     | query      | No       | integer |

##### Responses

| Code  | Description |
|-------|-------------|
| 200   | Success     |

```