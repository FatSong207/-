import md5 from 'js-md5'

/**
 * 獲取一個32位隨機字符串
 * @returns
 */
const GetRandomString = () => {
  var chars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz123456789'
  var maxPos = chars.length
  var pwd = ''
  for (var i = 0; i < 32; i++) {
    pwd += chars.charAt(Math.floor(Math.random() * maxPos))
  }
  return pwd
}

/**
* 簽名
* @param config 請求配置
* @param nonce 隨機字符串
* @param timestamp 簽名時間戳
* @param appSecret 應用密鑰
* @param method 請求方式
*/
const sign = (config, nonce, timestamp, appSecret) => {
  // 簽名格式： timestamp + nonce + data(字典升序)+appSecret
  const ret = []
  if (config.params) {
    const pArray = []
    const data = config.params
    for (const it in data) {
      pArray.push(it)
    }
    // 字典升序
    const sArray = pArray.sort()
    for (const it of sArray) {
      let val = data[it]
      if (typeof val === 'object' && //
        (!(val instanceof Array) || (val.length > 0 && (typeof val[0] === 'object')))) {
        val = JSON.stringify(val)
      }
      ret.push(it + val)
    }
  } else {
    ret.push(JSON.stringify(config.data))
  }
  const signsrc = timestamp + nonce + ret.join('') + appSecret
  return md5(signsrc)
}

export {
  sign,
  GetRandomString
}
