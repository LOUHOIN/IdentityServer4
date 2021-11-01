# IdentityServer4
* IDS4基于OPenID Connect（OIDC）、OAuth2.0
* OAuth2.0：一种授权协议，目前Web的安全手段之一

## Authorization Server 授权服务器
* 被资源信任，可以发布安全凭据（Access Token）给客户端应用
* ![](./img/01.png)
## 

## Authorization Grant（授权类型）
### Authorization Code（授权码）
* 使用授权服务器，不是直接从Owner那里获得，代表资源所有者委托给应用的权限。可以对客户端应用进行身份认证，直接发送到客户端应用，不经过资源所有者的浏览器，不会暴露access token
### Implicit（简化授权）
* 浏览器内的客户端应用，没有授权码发回到客户端，而是授权服务器直接发送access token到保护资源，没有对客户端服务器进行身份认证，但是可以带着access token重定向回客户端的uri进行验证Resource Owner Password Credentials（资源所有者密码）
* 当资源所有者和客户端构筑信任，且其他方式不可忍的时候，一次请求，会获取到access token和refresh token，当访问令牌过期的时候用刷新令牌获取新的访问令牌
### Client Credentials
* 资源不属于某个人或者用户

## Endpoint（端点）
### Authorization Endpoint（授权端点）
* 在机器交互，通过软件，客户端应用软件通过token端点展示授权
### Token Endpoint（Token端点）
* 在机器交互，通过软件，客户端应用软件通过token端点展示授权
### Scope（范围）
* 代表资源所有者在被保护资源里面的一些权限，可以将被保护资源分成不同的Scope，不同的资源所有者和用户就能有不同的权限
### Access Token
* 用来访问被保护资源的凭据（字符串组成）
* 代表了给客户端颁发的授权，也就是委托给客户端的权限
* 要描述资源所有者授予的Scope和有效期
* 授权服务器——颁发Access Token，客户端应用——展示Access Token给被保护资源，被保护资源——验证Access Token
### Refresh Token
* 用来获取Access Token的凭据，不会发往被保护资源
* 由Authorization Server颁发给客户端应用的
* 是个可选项
* 具备让客户端应用逐渐降低访问权限的能力（Scope越来越小）
