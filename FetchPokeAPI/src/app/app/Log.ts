import { Logger } from 'tslog';

/**
 * This class is designed to be a simple console logger.
 */
export class Log {

  private static logger = new Logger({
    displayFilePath: 'hidden',
    displayFunctionName: false
  });

  /**
   * Log in silly mode.
   *
   * @param args Content to log.
   */
  public static silly(...args: unknown[]): void {
    this.logger.silly(...args);
  }

  /**
   * Log in debug mode.
   *
   * @param args Content to log.
   */
  public static debug(...args: unknown[]): void {
    this.logger.debug(...args);
  }

  /**
   * Log in info mode.
   *
   * @param args Content to log.
   */
  public static info(...args: unknown[]): void {
    this.logger.info(...args);
  }

  /**
   * Log in error mode.
   *
   * @param args Content to log.
   */
  public static error(...args: unknown[]): void {
    this.logger.error(...args);
  }
}
