/**
 * Created by PanJiaChen on 16/11/18.
 */

/**
 * @param {string} path
 * @returns {Boolean}
 */
export function isExternal(path) {
  return /^(https?:|mailto:|tel:)/.test(path)
}

/**
 * @param {string} str
 * @returns {Boolean}
 */
export function validUsername(str) {
  const valid_map = ['admin', 'editor']
  return valid_map.indexOf(str.trim()) >= 0
}

/**
 * 驗證是否空
 * @param {string} str
 * @returns {Boolean}
 */
export function validempty(str) {
  return str.trim().length === 0
}
/**
 * @param {string} url
 * @returns {Boolean}
 */
export function validURL(url) {
  const reg = /^(https?|ftp):\/\/([a-zA-Z0-9.-]+(:[a-zA-Z0-9.&%$-]+)*@)*((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9][0-9]?)(\.(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])){3}|([a-zA-Z0-9-]+\.)*[a-zA-Z0-9-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{2}))(:[0-9]+)*(\/($|[a-zA-Z0-9.,?'\\+&%$#=~_-]+))*$/
  return reg.test(url)
}

/**
 * @param {string} str
 * @returns {Boolean}
 */
export function validLowerCase(str) {
  const reg = /^[a-z]+$/
  return reg.test(str)
}

/**
 * @param {string} str
 * @returns {Boolean}
 */
export function validUpperCase(str) {
  const reg = /^[A-Z]+$/
  return reg.test(str)
}

/**
 * @param {string} str
 * @returns {Boolean}
 */
export function validAlphabets(str) {
  const reg = /^[A-Za-z]+$/
  return reg.test(str)
}

/**
 * @param {string} email
 * @returns {Boolean}
 */
export function validEmail(email) {
  const reg = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
  return reg.test(email)
}

/**
 * @param {string} str
 * @returns {Boolean}
 */
export function isString(str) {
  if (typeof str === 'string' || str instanceof String) {
    return true
  }
  return false
}

/**
 * @param {Array} arg
 * @returns {Boolean}
 */
export function isArray(arg) {
  if (typeof Array.isArray === 'undefined') {
    return Object.prototype.toString.call(arg) === '[object Array]'
  }
  return Array.isArray(arg)
}

/**
 * @param {rule} arg
 * @param {value} arg
 * @param {callback} arg
 * @returns {value}
 */
export const validateCell = (rule, value, callback) => {
  const CellReg = /^09\d{2}\d{6}$/
  if (!value) {
    return callback(new Error('請輸入手機號碼'))
  }
  setTimeout(() => {
    if (CellReg.test(value)) {
      callback()
    } else {
      callback()
    }
  }, 100)
}

/**
 * @param {rule} arg
 * @param {value} arg
 * @param {callback} arg
 * @returns {value}
 */
export const validateCellReg = (rule, value, callback) => {
  const CellReg = /^09\d{2}\d{6}$/
  if (value) {
    if (CellReg.test(value)) {
      callback()
    } else {
      callback(new Error('請輸入正確的手機號碼'))
    }
  } else {
    if (rule.required) {
      callback(new Error('請輸入手機號碼'))
    } else {
      callback()
    }
  }
}

/**
 * @param {rule} arg
 * @param {value} arg
 * @param {callback} arg
 * @returns {value}
 */
export const validateEamilReg = (rule, value, callback) => {
  if (value) {
    if (validEmail(value)) {
      callback()
    } else {
      callback(new Error('請輸入正確的電子郵件'))
    }
  } else {
    if (rule.required) {
      callback(new Error('請輸入電子郵件'))
    } else {
      callback()
    }
  }
}

export const validateID = (ID) => {
  var city = [1, 10, 19, 28, 37, 46, 55, 64, 39, 73, 82, 2, 11, 20, 48, 29, 38, 47, 56, 65, 74, 83, 21, 3, 12, 30];
  ID = ID.toUpperCase();
  var total = 0;
  // 使用「正規表達式」檢驗格式
  if (!ID.match(/^[A-Z]\d{9}$/) && !ID.match(/^[A-Z][A-D]\d{8}$/)) {
    // callback(new Error('請輸入正確的身分證字號或居留證號'))
    total = 1;
  } else {
    if (ID.match(/^[A-Z]\d{9}$/)) { // 身分證字號
      // 將字串分割為陣列(IE必需這麼做才不會出錯)
      ID = ID.split('');
      // 計算總分
      total = city[ID[0].charCodeAt(0) - 65];
      for (var i = 1; i <= 8; i++) {
        total += eval2(ID[i]) * (9 - i);
      }
    } else { // 外來人口統一證號
      // 將字串分割為陣列(IE必需這麼做才不會出錯)
      ID = ID.split('');
      // 計算總分
      total = city[ID[0].charCodeAt(0) - 65];
      // 外來人口的第2碼為英文A-D(10~13)，這裡把他轉為區碼並取個位數，之後就可以像一般身分證的計算方式一樣了。
      ID[1] = ID[1].charCodeAt(0) - 65;
      for (var n = 1; n <= 8; n++) {
        total += eval2(ID[n]) * (9 - n);
      }
    }
    // 補上檢查碼(最後一碼)
    total += eval2(ID[9]);

    return total
  }
}

/**
 * 驗證身分證字號或居留證號
 * @param {rule} arg
 * @param {value} arg
 * @param {callback} arg
 * @returns {value}
 */
export const validateIDReg = (rule, value, callback) => {
  if (value) {
    if (value.length === 10) {
      const total = validateID(value);
      // 檢查比對碼(餘數應為0);
      if (total % 10 === 0) {
        callback()
      } else {
        callback(new Error('請輸入正確的身分證字號或居留證號'))
      }
    } else {
      callback(new Error('請輸入正確的身分證字號或居留證號'))
    }
  } else {
    if (rule.required) {
      callback(new Error('請輸入身分證字號或居留證號'))
    } else {
      callback()
    }
  }
}

export const validateBusinessID = (ID) => {
  var invalidList = '00000000,11111111';
  var validateOperator = [1, 2, 1, 2, 1, 2, 4, 1];
  var sum = 0;
  if (!/^\d{8}$/.test(ID) || invalidList.indexOf(ID) !== -1) {
    sum = 1
  }

  var calculate = (product) => { // 個位數 + 十位數
    var ones = product % 10;
    var tens = (product - ones) / 10;
    return ones + tens;
  };

  for (var i = 0; i < validateOperator.length; i++) {
    sum += calculate(ID[i] * validateOperator[i]);
  }

  return sum
}

/**
 * 驗證統一編號
 * @param {rule} arg
 * @param {value} arg
 * @param {callback} arg
 * @returns {value}
 */
export const validateBusinessIDReg = (rule, value, callback) => {
  if (value) {
    if (value.length === 8) {
      const sum = validateBusinessID(value)

      if (sum % 10 === 0 || (value[6] === '7' && (sum + 1) % 10 === 0)) {
        callback()
      } else {
        callback(new Error('請輸入正確的統一編號'))
      }
    } else {
      callback(new Error('請輸入正確的統一編號'))
    }
  } else {
    if (rule.required) {
      callback(new Error('請輸入統一編號'))
    } else {
      callback()
    }
  }
}

export const validateIDCombo = (rule, value, callback) => {
  if (value) {
    let total = 0
    if (value.match(/^[A-Z].*?$/)) {
      if (value.length === 10) {
        total = validateID(value);
        // 檢查比對碼(餘數應為0);
        if (total % 10 === 0) {
          callback()
        } else {
          callback(new Error('請輸入正確的身分證字號或居留證號'))
        }
      } else {
        callback(new Error('請輸入正確的身分證字號或居留證號'))
      }
    } else if (value.match(/^\d.*?$/)) {
      if (value.length === 8) {
        total = validateBusinessID(value)
        if (total % 10 === 0 || (value[6] === '7' && (total + 1) % 10 === 0)) {
          callback()
        } else {
          callback(new Error('請輸入正確的統一編號'))
        }
      } else {
        callback(new Error('請輸入正確的統一編號'))
      }
    } else {
      callback(new Error('請輸入正確的身分證字號、居留證號或統一編號'))
    }
  } else {
    if (rule.required) {
      callback(new Error('請輸入身分證字號、居留證號或統一編號'))
    } else {
      callback()
    }
  }
}

function eval2(str) {
  var Fn = Function;
  return new Fn('return ' + str)();
}
