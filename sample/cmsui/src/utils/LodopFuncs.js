// ==本JS是加載Lodop插件及CLodop服務的綜合示例，可直接使用，建議看懂后融進自己頁面程序==

var CreatedOKLodopObject, CLodopIsLocal, CLodopJsState

// ==判斷是否需要CLodop(那些不支持插件的瀏覽器):==
function needCLodop() {
  try {
    var ua = navigator.userAgent
    if (ua.match(/Windows\sPhone/i)) { return true }
    if (ua.match(/iPhone|iPod|iPad/i)) { return true }
    if (ua.match(/Android/i)) { return true }
    if (ua.match(/Edge\D?\d+/i)) { return true }

    var verTrident = ua.match(/Trident\D?\d+/i)
    var verIE = ua.match(/MSIE\D?\d+/i)
    var verOPR = ua.match(/OPR\D?\d+/i)
    var verFF = ua.match(/Firefox\D?\d+/i)
    var x64 = ua.match(/x64/i)
    if ((!verTrident) && (!verIE) && (x64)) { return true } else if (verFF) {
      verFF = verFF[0].match(/\d+/)
      if ((verFF[0] >= 41) || (x64)) { return true }
    } else if (verOPR) {
      verOPR = verOPR[0].match(/\d+/)
      if (verOPR[0] >= 32) { return true }
    } else if ((!verTrident) && (!verIE)) {
      var verChrome = ua.match(/Chrome\D?\d+/i)
      if (verChrome) {
        verChrome = verChrome[0].match(/\d+/)
        if (verChrome[0] >= 41) { return true }
      }
    }
    return false
  } catch (err) {
    return true
  }
}

// ==加載引用CLodop的主JS,用雙端口8000和18000(以防其中一個被占):==
function loadCLodop() {
  if (CLodopJsState === 'loading' || CLodopJsState === 'complete') return
  CLodopJsState = 'loading'
  var head = document.head || document.getElementsByTagName('head')[0] || document.documentElement
  var JS1 = document.createElement('script')
  var JS2 = document.createElement('script')
  JS1.src = 'http://localhost:8000/CLodopfuncs.js?priority=1'
  JS2.src = 'http://localhost:18000/CLodopfuncs.js'
  JS1.onload = JS2.onload = function() { CLodopJsState = 'complete' }
  JS1.onerror = JS2.onerror = function(evt) { CLodopJsState = 'complete' }
  head.insertBefore(JS1, head.firstChild)
  head.insertBefore(JS2, head.firstChild)
  CLodopIsLocal = !!((JS1.src + JS2.src).match(/\/\/localho|\/\/127.0.0./i))
}

if (needCLodop()) { loadCLodop() }// 加載

// ==獲取LODOP對象主過程,判斷是否安裝、需否升級:==
function getLodop(oOBJECT, oEMBED) {
  var strHtmInstall = "<br><font color='#FF00FF'>打印控件未安裝!點擊這里<a href='install_lodop32.exe' target='_self'>執行安裝</a>,安裝后請刷新頁面或重新進入。</font>"
  var strHtmUpdate = "<br><font color='#FF00FF'>打印控件需要升級!點擊這里<a href='install_lodop32.exe' target='_self'>執行升級</a>,升級后請重新進入。</font>"
  var strHtm64_Install = "<br><font color='#FF00FF'>打印控件未安裝!點擊這里<a href='install_lodop64.exe' target='_self'>執行安裝</a>,安裝后請刷新頁面或重新進入。</font>"
  var strHtm64_Update = "<br><font color='#FF00FF'>打印控件需要升級!點擊這里<a href='install_lodop64.exe' target='_self'>執行升級</a>,升級后請重新進入。</font>"
  var strHtmFireFox = "<br><br><font color='#FF00FF'>（注意：如曾安裝過Lodop舊版附件npActiveXPLugin,請在【工具】->【附加組件】->【擴展】中先卸它）</font>"
  var strHtmChrome = "<br><br><font color='#FF00FF'>(如果此前正常，僅因瀏覽器升級或重安裝而出問題，需重新執行以上安裝）</font>"
  var strCLodopInstall_1 = "<br><font color='#FF00FF'>Web打印服務CLodop未安裝啟動，點擊這里<a href='CLodop_Setup_for_Win32NT.exe' target='_self'>下載執行安裝</a>"
  var strCLodopInstall_2 = "<br>（若此前已安裝過，可<a href='CLodop.protocol:setup' target='_self'>點這里直接再次啟動</a>）"
  var strCLodopInstall_3 = '，成功后請刷新本頁面。</font>'
  var strCLodopUpdate = "<br><font color='#FF00FF'>Web打印服務CLodop需升級!點擊這里<a href='CLodop_Setup_for_Win32NT.exe' target='_self'>執行升級</a>,升級后請刷新頁面。</font>"
  var LODOP
  try {
    var ua = navigator.userAgent
    var isIE = !!(ua.match(/MSIE/i)) || !!(ua.match(/Trident/i))
    if (needCLodop()) {
      try {
        LODOP = getCLodop()
      } catch (err) { }
      if (!LODOP && CLodopJsState !== 'complete') {
        if (CLodopJsState === 'loading') alert('網頁還沒下載完畢，請稍等一下再操作.'); else alert('沒有加載CLodop的主js，請先調用loadCLodop過程.')
        return
      }
      if (!LODOP) {
        document.body.innerHTML = strCLodopInstall_1 + (CLodopIsLocal ? strCLodopInstall_2 : '') + strCLodopInstall_3 + document.body.innerHTML
        return
      } else {
        if (CLODOP.CVERSION < '3.0.9.3') {
          document.body.innerHTML = strCLodopUpdate + document.body.innerHTML
        }
        if (oEMBED && oEMBED.parentNode) { oEMBED.parentNode.removeChild(oEMBED) }
        if (oOBJECT && oOBJECT.parentNode) { oOBJECT.parentNode.removeChild(oOBJECT) }
      }
    } else {
      var is64IE = isIE && !!(ua.match(/x64/i))
      // ==如果頁面有Lodop就直接使用,否則新建:==
      if (oOBJECT || oEMBED) {
        if (isIE) { LODOP = oOBJECT } else { LODOP = oEMBED }
      } else if (!CreatedOKLodopObject) {
        LODOP = document.createElement('object')
        LODOP.setAttribute('width', 0)
        LODOP.setAttribute('height', 0)
        LODOP.setAttribute('style', 'position:absolute;left:0px;top:-100px;width:0px;height:0px;')
        if (isIE) { LODOP.setAttribute('classid', 'clsid:2105C259-1E0C-4534-8141-A753534CB4CA') } else { LODOP.setAttribute('type', 'application/x-print-lodop') }
        document.documentElement.appendChild(LODOP)
        CreatedOKLodopObject = LODOP
      } else { LODOP = CreatedOKLodopObject }
      // ==Lodop插件未安裝時提示下載地址:==
      if ((!LODOP) || (!LODOP.VERSION)) {
        if (ua.indexOf('Chrome') >= 0) { document.body.innerHTML = strHtmChrome + document.body.innerHTML }
        if (ua.indexOf('Firefox') >= 0) { document.body.innerHTML = strHtmFireFox + document.body.innerHTML }
        document.body.innerHTML = (is64IE ? strHtm64_Install : strHtmInstall) + document.body.innerHTML
        return LODOP
      }
    }
    if (LODOP.VERSION < '6.2.2.6') {
      if (!needCLodop()) { document.body.innerHTML = (is64IE ? strHtm64_Update : strHtmUpdate) + document.body.innerHTML }
    }
    // ===如下空白位置適合調用統一功能(如注冊語句、語言選擇等):==

    // =======================================================
    return LODOP
  } catch (err) {
    alert('getLodop出錯:' + err)
  }
}
export { getLodop } // 導出getLodop
