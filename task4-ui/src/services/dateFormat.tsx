export const dateFormat = (date: Date, fstr: string, utc: boolean): string => {
  return fstr.replace(/%[YmdHMS]/g, function (m: string): string {
    switch (m) {
      case "%Y":
        return String(utc ? date.getUTCFullYear() : date.getFullYear());
      case "%m":
        m = String(1 + (utc ? date.getUTCMonth() : date.getMonth()));
        break;
      case "%d":
        m = String(utc ? date.getUTCDate() : date.getDate());
        break;
      case "%H":
        m = String(utc ? date.getUTCHours() : date.getHours());
        break;
      case "%M":
        m = String(utc ? date.getUTCMinutes() : date.getMinutes());
        break;
      case "%S":
        m = String(utc ? date.getUTCSeconds() : date.getSeconds());
        break;
      default:
        return m.slice(1);
    }
    return ("0" + m).slice(-2);
  });
  // dateFormat(new Date(order.created_at), '%Y-%m-%d %H:%M:%S', true)
};
