import { AxiosInstance } from 'axios';
import { RedisStore, setup } from 'axios-cache-adapter';
import axiosRetry from 'axios-retry';
import * as redis from 'redis';
import { RedisClient } from 'redis';

/**
 * This class is designed to fetch API endpoints.
 */
export class Fetcher {

  private static _axiosInstance: AxiosInstance;
  private static readonly RETRIES_BEFORE_FAILING = 10;

  static async setUp(): Promise<void> {
    // noinspection JSUnusedLocalSymbols
    const never: never | Promise<any> = this.axiosInstance;
  }

  /**
   * Get the axios fetcher instance.
   */
  static get axiosInstance(): Promise<AxiosInstance> {
    return (async () => {
      if (!this._axiosInstance) {
        const store = await this.createRedisStore();
        this._axiosInstance = setup({
          headers: { 'Content-Type': 'application/json' },
          timeout: 1000 * 60 * 5, // 5 minutes,
          cache: {
            maxAge: 1000 * 60 * 60 * 24, // 24 hours
            store: store
          }
        });
        axiosRetry(this._axiosInstance, { retries: this.RETRIES_BEFORE_FAILING });
      }

      return this._axiosInstance;
    })();
  }

  private static async createRedisStore(): Promise<RedisStore> {
    const redisClient: RedisClient = redis.createClient({
      url: 'redis://redis-12151.c262.us-east-1-3.ec2.cloud.redislabs.com:12151',
      password: 'SndMILwCfMeHCMZTXRAVeNK2fYh4nNdU'
    });

    return new RedisStore(redisClient);
  }
}
