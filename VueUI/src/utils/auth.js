import Cookies from 'js-cookie'

const TokenKey = 'yuebon_soft_token' + (window.location.port ? '_' + window.location.port : '')

export function getToken() {
  // console.log(TokenKey)
  return Cookies.get(TokenKey)
}

export function setToken(token) {
  // console.log(TokenKey)
  return Cookies.set(TokenKey, token)
}

export function removeToken() {
  // console.log(TokenKey)
  return Cookies.remove(TokenKey)
}
