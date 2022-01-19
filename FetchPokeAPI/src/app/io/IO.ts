import fs from 'fs';

/**
 * This class is designed to write in a file.
 */
export class IO {

  /**
   * Write content in a file.
   *
   * @param filePath
   * @param content
   */
  public static write(filePath: string, content: string) {
    fs.writeFileSync(filePath, content);
  }

  /**
   * Read a file and parse his content to a JSON object.
   *
   * @param filePath
   */
  public static readAsJSONObject(filePath: string) {
    return JSON.parse(fs.readFileSync(filePath, 'utf-8'));
  }
}
