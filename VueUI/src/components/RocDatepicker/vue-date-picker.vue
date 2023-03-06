<template>
  <div class="datetime-picker">
    <input
      type="text"
      v-bind="inputAttr"
      :name="name"
      :style="inputStyle"
      :class="inputClass"
      :readonly="readonly"
      :value="value"
      @input="$emit('input', $event.target.value)"
      @click="show = !show"
    >
    <div
      v-show="show"
      class="picker-wrap"
      v-bind="calendarAttr"
      :style="calendarStyle"
      :class="calendarClass"
    >
      <table class="date-picker">
        <thead>
          <tr class="date-head">
            <th colspan="4">
              <span class="btn-prev" @click="yearClick(-1)" />
              <span class="show-year">民國{{ now.getFullYear()-1911 }}年</span>
              <span class="btn-next" @click="yearClick(1)" />
            </th>
            <th colspan="3">
              <span class="btn-prev" @click="monthClick(-1)" />
              <span class="show-month">{{ months[now.getMonth()] }}</span>
              <span class="btn-next" @click="monthClick(1)" />
            </th>
          </tr>
          <tr class="date-days">
            <th v-for="day in days" :key="day">{{ day }}</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(v, i) in 6" :key="i">
            <td
              v-for="(j) in 7"
              :key="j"
              :class="[
                date[i * 7 + j] ? date[i * 7 + j].status : '',
                { 'date-disabled': date[i * 7 + j] && date[i * 7 + j].disabled }
              ]"
              :date="date[i * 7 + j] && date[i * 7 + j].date"
              @click="date[i * 7 + j] && !date[i * 7 + j].disabled && pickDate(i * 7 + j)"
            >{{ date[i * 7 + j] && date[i * 7 + j].text }}
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    readonly: { type: Boolean, default: false },
    value: { type: String, default: '' },
    format: { type: String, default: 'YYYY-MM-DD' },
    name: { type: String, default: '' },
    // eslint-disable-next-line vue/require-default-prop
    inputAttr: Object,
    // eslint-disable-next-line vue/require-default-prop
    inputStyle: [Object, Array],
    // eslint-disable-next-line vue/require-default-prop
    inputClass: [Object, Array],
    // eslint-disable-next-line vue/require-default-prop
    calendarAttr: Object,
    // eslint-disable-next-line vue/require-default-prop
    calendarStyle: [Object, Array],
    // eslint-disable-next-line vue/require-default-prop
    calendarClass: [Object, Array],
    disabledDate: { type: Function, default: () => false }
  },
  data: function() {
    return {
      show: false,
      days: ['日', '一', '二', '三', '四', '五', '六'],
      months: ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月'],
      date: [],
      now: new Date(),
      pickedValue: { type: String, default: '' }
    }
  },
  watch: {
    now: function() {
      this.update()
    },
    show: function() {
      this.update()
    }
  },
  mounted: function() {
    this.pickedValue = this.value
    this.$nextTick(() => {
      this.now = this.parse(this.pickedValue) || new Date()
      document.addEventListener('click', this.leave, false)
    })
  },
  beforeDestroy: function() {
    document.removeEventListener('click', this.leave, false)
  },
  methods: {
    close: function() {
      this.show = false
    },
    update: function() {
      const arr = []
      const time = new Date(this.now)
      time.setMonth(time.getMonth(), 1) // the first day
      let curFirstDay = time.getDay()
      curFirstDay === 0 && (curFirstDay = 7)
      time.setDate(0) // the last day
      const lastDayCount = time.getDate()
      for (let i = curFirstDay; i > 0; i--) {
        const tmpTime = new Date(time.getFullYear(), time.getMonth(), lastDayCount - i + 1)
        arr.push({
          text: lastDayCount - i + 1,
          time: tmpTime,
          status: 'date-pass',
          disabled: this.disabledDate(tmpTime)
        })
      }

      time.setMonth(time.getMonth() + 2, 0) // the last day of this month
      const curDayCount = time.getDate()
      time.setDate(1) // fix bug when month change
      const value = this.pickedValue || this.stringify(new Date())
      for (let i = 0; i < curDayCount; i++) {
        const tmpTime = new Date(time.getFullYear(), time.getMonth(), i + 1)
        let status = ''
        this.stringify(tmpTime) === value && (status = 'date-active')
        arr.push({
          text: i + 1,
          time: tmpTime,
          status: status,
          disabled: this.disabledDate(tmpTime)
        })
      }

      let j = 1
      while (arr.length < 42) {
        const tmpTime = new Date(time.getFullYear(), time.getMonth() + 1, j)
        arr.push({
          text: j,
          time: tmpTime,
          status: 'date-future',
          disabled: this.disabledDate(tmpTime)
        })
        j++
      }
      this.date = arr
    },
    yearClick: function(flag) {
      this.now.setFullYear(this.now.getFullYear() + flag)
      this.now = new Date(this.now)
    },
    monthClick: function(flag) {
      this.now.setMonth(this.now.getMonth() + flag, 1)
      this.now = new Date(this.now)
    },
    pickDate: function(index) {
      this.show = false
      this.now = new Date(this.date[index].time)
      this.pickedValue = this.stringify()
      this.$emit('input', this.pickedValue)
    },
    parse: function(str) {
      const time = new Date(str)
      return isNaN(time.getTime()) ? null : time
    },
    stringify: function(time = this.now, format = this.format) {
      const year = time.getFullYear()
      const month = time.getMonth() + 1
      const date = time.getDate()
      const monthName = this.months[time.getMonth()]

      const map = {
        YYYY: year - 1911,
        MMM: monthName,
        MM: ('0' + month).slice(-2),
        M: month,
        DD: ('0' + date).slice(-2),
        D: date
      }
      return format.replace(/Y+|M+|D+/g, function(str) {
        return map[str]
      })
    },
    leave: function(e) {
      if (!this.$el.contains(e.target)) {
        this.close()
      }
    }
  }
}
</script>

<style scoped>
.datetime-picker {
  position: relative;
  display: inline-block;
  font-family: "Segoe UI", "Lucida Grande", Helvetica, Arial, "Microsoft YaHei";
  -webkit-font-smoothing: antialiased;
  color: #333;
}

.datetime-picker * {
  box-sizing: border-box;
}

.datetime-picker input {
  width: 238px;
  padding: 5px 10px;
  height: 30px;
  outline: 0 none;
  border: 1px solid #ccc;
  font-size: 13px;
}

.datetime-picker .picker-wrap {
  position: relative;
  z-index: 1000;
  width: 238px;
  height: 280px;
  margin-top: 2px;
  background-color: #fff;
  box-shadow: 0 0 6px #ccc;
}

.datetime-picker table {
  width: 100%;
  border-collapse: collapse;
  border-spacing: 0;
  text-align: center;
  font-size: 13px;
}

.datetime-picker tr {
  height: 34px;
  border: 0 none;
}

.datetime-picker th, .datetime-picker td {
  overflow: hidden;
  user-select: none;
  width: 34px;
  height: 34px;
  padding: 0;
  border: 0 none;
  line-height: 34px;
  text-align: center;
}

.datetime-picker td {
  cursor: pointer;
}

.datetime-picker td:hover {
  background-color: #f0f0f0;
}

.datetime-picker .date-disabled:not(.date-active) {
  cursor: not-allowed;
  color: #ccc;
}

.datetime-picker .date-disabled:not(.date-active):hover {
  background-color: transparent;
}

.datetime-picker .date-pass, .datetime-picker .date-future {
  color: #aaa;
}

.datetime-picker .date-active,
.datetime-picker .date-active:hover {
  background-color: #ececec;
  color: #3bb4f2;
}

.datetime-picker .date-head {
  background-color: #3bb4f2;
  text-align: center;
  color: #fff;
  font-size: 14px;
}

.datetime-picker .date-days {
  color: #3bb4f2;
  font-size: 14px;
}

.datetime-picker .show-year {
  display: inline-block;
  min-width: 66px;
}

.datetime-picker .show-month {
  display: inline-block;
  min-width: 32px;
}

.datetime-picker .btn-prev,
.datetime-picker .btn-next {
  cursor: pointer;
  padding: 8px 11px;
}

.datetime-picker .btn-prev::after,
.datetime-picker .btn-next::after {
  position: relative;
  content: "";
  left: 2px;
  display: inline-block;
  width: 7px;
  height: 7px;
  border-top: 1px solid #fff;
  border-left: 1px solid #fff;
  transform: rotate(-45deg);
}

.datetime-picker .btn-next::after {
  left: -2px;
  border: 0 none;
  border-right: 1px solid #fff;
  border-bottom: 1px solid #fff;
}

.datetime-picker .btn-prev:hover,
.datetime-picker .btn-next:hover {
  background: rgba(16, 160, 234, 0.5);
}
</style>
