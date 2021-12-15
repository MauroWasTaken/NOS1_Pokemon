import axios, { AxiosInstance } from 'axios';
import { setupCache } from 'axios-cache-adapter';
import axiosRetry from 'axios-retry';

/**
 * This class is designed to fetch API endpoints.
 */
export class Fetcher {

  private static _axiosInstance: AxiosInstance;
  private static readonly RETRIES_BEFORE_FAILING = 10;

  /**
   * Get the axios fetcher instance.
   */
  static get axiosInstance() {
    if (!this._axiosInstance) {
      this._axiosInstance = axios.create({
        headers: { 'Content-Type': 'application/json' },
        adapter: setupCache({
          maxAge: 1000 * 60 * 60 * 24 // 24 hours
        }).adapter,
        timeout: 1000 * 60 * 5 // 5 minutes
      });

      axiosRetry(this._axiosInstance, {
        retries: this.RETRIES_BEFORE_FAILING,
      });
    }

    return this._axiosInstance;
  }
}
