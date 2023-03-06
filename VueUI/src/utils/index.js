/**
 * Parse the time to string
 * @param {(Object|string|number)} time
 * @param {string} cFormat
 * @returns {string | null}
 */
export function parseTime(time, cFormat) {
  if (arguments.length === 0) {
    return null
  }
  const format = cFormat || '{y}-{m}-{d} {h}:{i}:{s}'
  let date
  if (typeof time === 'object') {
    date = time
  } else {
    if ((typeof time === 'string') && (/^[0-9]+$/.test(time))) {
      time = parseInt(time)
    }
    if ((typeof time === 'number') && (time.toString().length === 10)) {
      time = time * 1000
    }
    date = new Date(time)
  }
  const formatObj = {
    y: date.getFullYear(),
    c: date.getFullYear() - 1911,
    m: date.getMonth() + 1,
    d: date.getDate(),
    h: date.getHours(),
    i: date.getMinutes(),
    s: date.getSeconds(),
    ms: date.getMilliseconds(),
    a: date.getDay()
  }
  const time_str = format.replace(/{([ycmdhisa])+}/g, (result, key) => {
    const value = formatObj[key]
    // Note: getDay() returns 0 on Sunday
    if (key === 'a') { return ['日', '一', '二', '三', '四', '五', '六'][value] }
    return value.toString().padStart(2, '0')
  })
  return time_str
}

/**
 * @param {number} time
 * @param {string} option
 * @returns {string}
 */
export function formatTime(time, option) {
  if (('' + time).length === 10) {
    time = parseInt(time) * 1000
  } else {
    time = +time
  }
  const d = new Date(time)
  const now = Date.now()

  const diff = (now - d) / 1000

  if (diff < 30) {
    return '剛剛'
  } else if (diff < 3600) {
    // less 1 hour
    return Math.ceil(diff / 60) + '分鐘前'
  } else if (diff < 3600 * 24) {
    return Math.ceil(diff / 3600) + '小時前'
  } else if (diff < 3600 * 24 * 2) {
    return '1天前'
  }
  if (option) {
    return parseTime(time, option)
  } else {
    return (
      d.getMonth() +
      1 +
      '月' +
      d.getDate() +
      '日' +
      d.getHours() +
      '時' +
      d.getMinutes() +
      '分'
    )
  }
}

/**
 * @param {string} url
 * @returns {Object}
 */
export function param2Obj(url) {
  const search = url.split('?')[1]
  if (!search) {
    return {}
  }
  return JSON.parse(
    '{"' +
    decodeURIComponent(search)
      .replace(/"/g, '\\"')
      .replace(/&/g, '","')
      .replace(/=/g, '":"')
      .replace(/\+/g, ' ') +
    '"}'
  )
}

/**
 * 獲取url參數值
 * @param {url參數名稱} name
 */
export function getUrlKey(name) {
  // eslint-disable-next-line no-sparse-arrays
  return decodeURIComponent((new RegExp('[?|&]' + name + '=' + '([^&;]+?)(&|#|;|$)').exec(location.href) || [, ''])[1].replace(/\+/g, '%20')) || null
}

/**
 * 下載文件調用
 * @param 接口返回數據 文件名
 */
export function downloadFile(resUrl, fileName) {
  if (!resUrl) {
    return
  }
  // 創建下載鏈接
  const url = resUrl
  const link = document.createElement('a')
  link.style.display = 'none'
  link.href = url
  link.setAttribute('download', fileName)// 文件名
  document.body.appendChild(link)
  link.click()
  document.body.removeChild(link) // 下載完成移除元素
  window.URL.revokeObjectURL(url) // 釋放掉blob對象
}

// 表單重置
export function resetForm(refName) {
  if (this.$refs[refName]) {
    this.$refs[refName].resetFields()
  }
}

/**
 * Check if an element has a class
 * @param {HTMLElement} elm
 * @param {string} cls
 * @returns {boolean}
 */
export function hasClass(ele, cls) {
  return !!ele.className.match(new RegExp('(\\s|^)' + cls + '(\\s|$)'))
}
/**
 * Add class to element
 * @param {HTMLElement} elm
 * @param {string} cls
 */
export function addClass(ele, cls) {
  if (!hasClass(ele, cls)) ele.className += ' ' + cls
}

/**
 * Remove class from element
 * @param {HTMLElement} elm
 * @param {string} cls
 */
export function removeClass(ele, cls) {
  if (hasClass(ele, cls)) {
    const reg = new RegExp('(\\s|^)' + cls + '(\\s|$)')
    ele.className = ele.className.replace(reg, ' ')
  }
}

export function isYes(yn) {
  if (!yn) {
    return false;
  } else if (yn === '/Yes') {
    return true;
  } else {
    return false;
  }
}

export function mergerAddress(addarry) {
  if (!addarry) {
    return '陳列不能為空值'
  } else if (addarry.length !== 17) {
    return '地址陣列不為17';
  } else {
    const addarryNew = [
      addarry[0],
      isYes(addarry[1]) ? '縣' : '',
      isYes(addarry[2]) ? '市' : '',
      addarry[3],
      isYes(addarry[4]) ? '鄉' : '',
      isYes(addarry[5]) ? '市' : '',
      isYes(addarry[6]) ? '鎮' : '',
      isYes(addarry[7]) ? '區' : '',
      addarry[8],
      isYes(addarry[9]) ? '街' : '',
      isYes(addarry[10]) ? '路' : '',
      addarry[11],
      (!addarry[11]) ? '' : '段',
      addarry[12],
      (!addarry[12]) ? '' : '巷',
      addarry[13],
      (!addarry[13]) ? '' : '弄',
      addarry[14],
      (!addarry[14]) ? '' : '號',
      addarry[15],
      (!addarry[15]) ? '' : '樓',
      // (!addarry[16]) ? '' : '之',
      addarry[16]
    ];
    return addarryNew.join('');
  }
}

/**
 * 將民國年字串轉換成西元年字串(111-07-28 => 2022-07-28)
 * @param {HTMLElement} elm
 * @param {string} cls
 */
export function ROCDateToDate(ROCDateString) {
  var result = ''
  if (ROCDateString !== '' && ROCDateString !== null && ROCDateString !== undefined && ROCDateString.split('-').length === 3) {
    var year = ROCDateString.split('-')[0];
    year = parseInt(year) + 1911;
    result = year + '-' + ROCDateString.split('-')[1] + '-' + ROCDateString.split('-')[2];
    return result
  } else {
    result = ''
  }
}
