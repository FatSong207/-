<script>
export default {
  name: 'MenuItem',
  functional: true,
  props: {
    icon: {
      type: String,
      default: ''
    },
    title: {
      type: String,
      default: ''
    },
    isshownotifydot: {
      type: Boolean,
      default: false
    }
  },
  render(h, context) {
    const { icon, title, isshownotifydot } = context.props;
    const vnodes = [];

    if (icon) {
      vnodes.push(<svg-icon icon-class={icon} />);
    }
    if (isshownotifydot) {
      vnodes.push(<el-badge is-dot class='item'></el-badge>);
    }
    if (title) {
      vnodes.push(<span slot='title'>{title}</span>);
    }

    return vnodes;
  }
};
</script>
<style scoped>
.item {
  margin-right: 5px;
}
</style>
